using MedicalResearch.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalResearch
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				var dbContext = services.GetRequiredService<ApplicationDbContext>();
				await Initializer.InitializeMedicineTypesContainersDosageFormsAsync(dbContext);

				var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
				await Initializer.InitializeRolesAsync(roleManager);

				var userManager = services.GetRequiredService<UserManager<User>>();
				await Initializer.InitializeAdminAsync(userManager);
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
