using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leave_management.Data;

namespace leave_management
{
    public static class SeedData
    {
        public static void Seed(UserManager<Employee> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            SeedRole(roleManager);
            SeedUser(userManager);
        }

        private static void SeedUser(UserManager<Employee> userManager)
        {
            if(userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new Employee
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                var result = userManager.CreateAsync(user, "Test@1234").Result;
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
        private static void SeedRole(RoleManager<IdentityRole> roleManager)
        {
            if(roleManager.RoleExistsAsync("Administrator").Result == false)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.CreateAsync(role).Wait();
            }
           if(roleManager.RoleExistsAsync("Employee").Result == false)
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                roleManager.CreateAsync(role).Wait();
            }

        }
    }
}
