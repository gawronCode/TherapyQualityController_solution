using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TherapyQualityController.Models;

namespace TherapyQualityController
{
    public static class InitialUserDataSeed
    {
        public static void Seed(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);

        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result != null) return;
            var user = new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com"
            };
            var result = userManager.CreateAsync(user, "password").Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Administrator").Wait();
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            CreateRole(roleManager, "Administrator");
            CreateRole(roleManager, "Patient");
            CreateRole(roleManager, "Doctor");
        }

        private static void CreateRole(RoleManager<IdentityRole> roleManager,
            string roleName)
        {
            if(roleManager.RoleExistsAsync(roleName).Result) return;
            var role = new IdentityRole
            {
                Name = roleName
            };
            var result = roleManager.CreateAsync(role).Result;
        }

    }
}
