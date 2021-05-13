using MedicalResearch.Enums;
using MedicalResearch.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalResearch
{
	public class Initializer
	{
		public static async Task InitializeRolesAsync(RoleManager<ApplicationRole> roleManager)
		{
			foreach (string role in Enum.GetNames(typeof(Role)))
			{
				if (await roleManager.FindByNameAsync(role) == null)
				{
					await roleManager.CreateAsync(new ApplicationRole(role));
				}
			}
		}

		public static async Task InitializeAdminAsync(UserManager<User> userManager)
		{
			string adminEmail = "string";
			string password = "string11";

			if (await userManager.FindByNameAsync(adminEmail) == null)
			{
				User admin = new User
				{
					Email = adminEmail,
					UserName = adminEmail,
					FirstName = "Admin",
					LastName = "Admin",
					Initials = "AA",
					RoleId = (int)Role.Administrator
				};

				await userManager.CreateAsync(admin, password);
			}
		}

		public static async Task InitializeMedicineTypesContainersDosageFormsAsync(ApplicationDbContext dbContext)
		{
			if (!dbContext.MedicineTypes.Any())
			{
				await dbContext.MedicineTypes.AddAsync(new MedicineType() { Name = "A" });
				await dbContext.MedicineTypes.AddAsync(new MedicineType() { Name = "B" });
				await dbContext.MedicineTypes.AddAsync(new MedicineType() { Name = "C" });
			}
			if (!dbContext.DosageForms.Any())
			{
				await dbContext.DosageForms.AddAsync(new DosageForm() { Name = "capsule" });
				await dbContext.DosageForms.AddAsync(new DosageForm() { Name = "tablet" });
				await dbContext.DosageForms.AddAsync(new DosageForm() { Name = "ointment" });
			}
			if (!dbContext.Containers.Any())
			{
				await dbContext.Containers.AddAsync(new Container() { Name = "ampoule" });
				await dbContext.Containers.AddAsync(new Container() { Name = "blister" });
				await dbContext.Containers.AddAsync(new Container() { Name = "bottle" });
			}
			await dbContext.SaveChangesAsync();
		}
	}
}
