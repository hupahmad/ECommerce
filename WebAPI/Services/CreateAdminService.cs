using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class CreateAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        public CreateAdminService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task CreateRoles()
        {

            var _user = await _userManager.FindByEmailAsync("admin@email.com");

            // check if the user exists
            if (_user == null)
            {
                //Here you could create the super admin who will maintain the web app
                var powerUser = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@email.com",
                };
                string adminPassword = "Password_0";

                var createPowerUser = await _userManager.CreateAsync(powerUser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await _userManager.AddToRoleAsync(powerUser, "Admin");

                }
            }
        }
    }
}