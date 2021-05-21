using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MedicalResearch.Business.Services;
using MedicalResearch.Data;
using MedicalResearch.Data.Entities;
using MedicalResearch.MapperProfiles;
using MedicalResearch.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MedicalResearch.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(_ =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                return config.CreateMapper();
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationSchemes(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, o =>
            {
                o.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync,
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddTransient<IPasswordValidator<User>, PasswordPolicy<User>>();

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<ApplicationSignInManager<User>>()
                .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>()
                .AddUserManager<ApplicationUserManager<User>>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            });

            services.AddAuthenticationSchemes();

            return services;
        }

        public static IServiceCollection AddJsonStringEnumConverter(this IServiceCollection services)
        {
            services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            return services;
        }
    }
}
