using Demo.Entity;
using Demo.Services.Implement;
using Demo.Services.Interface;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DbAPIContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DbConnection"),
        opt => opt.EnableRetryOnFailure()
        );
});

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<DbAPIContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHangfire(x => x
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_110)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DbConnection"), new PostgreSqlStorageOptions
    {
        QueuePollInterval = TimeSpan.FromSeconds(30),
        UseNativeDatabaseTransactions = false,
        DistributedLockTimeout = TimeSpan.FromMinutes(5),
        InvisibilityTimeout = TimeSpan.FromMinutes(5),
    }));
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseHttpsRedirection();

// This comment to test merger
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseFileServer(new FileServerOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
//    RequestPath = new PathString("/Resources")
//});

app.UseAuthorization();

app.MapControllers();

app.MapHangfireDashboard();

app.UseHangfireDashboard();
BackgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
//RecurringJob.AddOrUpdate<IOrderService>("Create_Order_Monthly_Payment", x => x.CreateOrderMonthly(), Cron.Monthly);

app.Run();
