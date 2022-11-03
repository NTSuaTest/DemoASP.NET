﻿using Demo.DTOs;
using Demo.DTOs.User;
using Demo.Entity;
using Demo.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace Demo.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly DbAPIContext _dbAPIContext;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, DbAPIContext dbAPIContext, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _dbAPIContext = dbAPIContext;
            _roleManager = roleManager;
        }

        public async Task<ErrorModel> Register(RegisterModel req)
        {
            ErrorModel error = new ErrorModel();
            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user != null)
            {
                error.Add("Email already exists");
                return error;
            }

            var hasher = new PasswordHasher<User>();
            user = new User()
            {
                PasswordHash = hasher.HashPassword(null, req.Password),
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email,
                UserName = req.Email,
                DateCreated = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var role = await _roleManager.FindByIdAsync("client");
                if (role == null)
                {
                    role = new Role("Client");
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(user, role.Name);
                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return error;
            }
            error.Add("Register Failed");
            return error;
        }
    }
}
