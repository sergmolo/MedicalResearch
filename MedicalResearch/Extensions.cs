using AutoMapper;
using FluentValidation;
using MedicalResearch.Models;
using MedicalResearch.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MedicalResearch
{
	public static class Extensions
	{
		public static IServiceCollection AddAutoMapper(this IServiceCollection services)
		{
			var mapper = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			}).CreateMapper();
			services.AddSingleton(mapper);

			return services;
		}

		public static IServiceCollection ConfigureResponseStatusCodes(this IServiceCollection services)
		{
			services.ConfigureApplicationCookie(options =>
			{
				options.Events.OnRedirectToLogin = context =>
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					
					return Task.CompletedTask;
				};
				options.Events.OnRedirectToAccessDenied = context =>
				{
					context.Response.StatusCode = StatusCodes.Status403Forbidden;
					
					return Task.CompletedTask;
				};
			});

			return services;
		}

		public static IServiceCollection AddPasswordPolicy(this IServiceCollection services)
		{
			services.AddTransient<IPasswordValidator<User>, PasswordPolicy>()
				.AddIdentity<User, ApplicationRole>(options =>
				{
					options.Password.RequiredLength = 8;
					options.Password.RequireLowercase = false;
					options.Password.RequireUppercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireDigit = true;
					options.Lockout.MaxFailedAccessAttempts = 5;
				})
				.AddEntityFrameworkStores<ApplicationDbContext>();
			
			return services;
		}

		public static IServiceCollection AddJsonStringEnumConverter(this IServiceCollection services)
		{
			services.AddControllers()
				.AddJsonOptions(options =>
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			return services;
		}

		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedicalResearch", Version = "v1" });
			});

			return services;
		}

		public static IServiceCollection AddValidators(this IServiceCollection services)
		{
			services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

			return services;
		}
	}
}
