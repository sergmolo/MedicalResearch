using MedicalResearch.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalResearch
{
	public class AuthorizedAttribute : AuthorizeAttribute
	{
		public AuthorizedAttribute(params Role[] roles) : base()
		{
			var rolesAsStrings = roles?.Select(x => Enum.GetName(typeof(Role), x))
				?? new List<string>();
			Roles = string.Join(",", rolesAsStrings);
		}
	}
}
