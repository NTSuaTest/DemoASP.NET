using Demo.Entity;
using Demo.Services.Implement;
using Demo.Services.Interface;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
        Title = "Demo API", 
        Version = "v1.0.0",
        //TermsOfService = new Uri("https://localhost:5001"),
        //License = new OpenApiLicense
        //{
        //    Name = "Use under LICX",
        //    Url = new Uri("https://localhost:5001"),
        //}
    });
});

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DbAPIContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DbConnection"),
        opt => opt.EnableRetryOnFailure()
        );
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImageService>();

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        //ClockSkew = TimeSpan.Zero // remove delay of token when expire,
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseHttpsRedirection();

// This comment to test merger
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseFileServer(new FileServerOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
//    RequestPath = new PathString("/Resources")
//});

app.UseHttpsRedirection();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.MapHangfireDashboard();

app.UseHangfireDashboard();
BackgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
//RecurringJob.AddOrUpdate<IOrderService>("Create_Order_Monthly_Payment", x => x.CreateOrderMonthly(), Cron.Monthly);

app.Run();
