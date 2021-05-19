using MedicalResearch.Data.Entities;
using MedicalResearch.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalResearch.Configuration
{
    public class Initializer
    {
        public static async Task InitializeAdminAsync(UserManager<User> userManager)
        {
            string adminEmail = "string";
            string password = "string11";

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Initials = "AA",
                    PasswordCreatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    Role = Role.Administrator
                };

                var res = await userManager.CreateAsync(admin, password);
                if (!res.Succeeded) throw new Exception(string.Join('\n', res.Errors.Select(e => e.Code)));
            }
        }
    }
}
