using Microsoft.AspNetCore.Identity;
using TherapyQualityController.Models.DbModels;

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

        private static async void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result != null) return;
            var user = new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com"
            };
            var result = await userManager.CreateAsync(user, "password");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            CreateRole(roleManager, "Administrator");
            CreateRole(roleManager, "Patient");
            CreateRole(roleManager, "Doctor");
        }

        private static async void CreateRole(
            RoleManager<IdentityRole> roleManager,
            string roleName)
        {
            if(roleManager.RoleExistsAsync(roleName).Result) return;
            var role = new IdentityRole
            {
                Name = roleName
            };
            var result = await roleManager.CreateAsync(role);
        }

    }
}
