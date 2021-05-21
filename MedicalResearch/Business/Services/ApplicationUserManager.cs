using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace MedicalResearch.Business.Services
{
    public class ApplicationUserManager<TUser> : UserManager<TUser> where TUser : class
    {
        public ApplicationUserManager(
                IUserStore<TUser> store,
                IOptions<IdentityOptions> optionsAccessor,
                IPasswordHasher<TUser> passwordHasher,
                IEnumerable<IUserValidator<TUser>> userValidators,
                IEnumerable<IPasswordValidator<TUser>> passwordValidators,
                ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
                IServiceProvider services, 
                ILogger<UserManager<TUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators,
                  keyNormalizer, errors, services, logger)
        {
        }

        public override bool SupportsUserClaim => false;
    }
}
