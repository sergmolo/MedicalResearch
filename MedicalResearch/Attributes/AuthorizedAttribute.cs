using System;
using System.Collections.Generic;
using System.Linq;
using MedicalResearch.Data.Enums;
using Microsoft.AspNetCore.Authorization;

namespace MedicalResearch.Attributes
{
    public class AuthorizedAttribute : AuthorizeAttribute
    {
        public AuthorizedAttribute(params Role[] roles)
        {
            var rolesAsStrings = roles?.Select(x => Enum.GetName(typeof(Role), x))
                ?? new List<string>();
            Roles = string.Join(",", rolesAsStrings);
        }
    }
}
