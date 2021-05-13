using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MedicalResearch.Models
{
	public class ApplicationRole : IdentityRole<int>
	{
		public ApplicationRole(string roleName) : base(roleName)
		{
		}

		public ApplicationRole()
		{
		}

		public ICollection<User> Users { get; set; } = default!;
	}
}
