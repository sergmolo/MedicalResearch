using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MedicalResearch.Models;
using MedicalResearch.Requests;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace MedicalResearch
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>();
			
			services.AddMediatR(Assembly.GetExecutingAssembly());
			
			services.AddAutoMapper();
			
			services.AddHttpContextAccessor();

			services.AddScoped<IUserClaimsPrincipalFactory<User>, ClaimsPrincipalFactory>();

			services.AddPasswordPolicy();

			services.AddControllers()
				.AddFluentValidation();

			services.AddValidators();

			services.AddJsonStringEnumConverter();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();

			services.ConfigureResponseStatusCodes();

			services.AddSwagger();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MedicalResearch v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
