using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MedicalResearch.Models
{
	public class User : IdentityUser<int>
	{
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string Initials { get; set; } = default!;
		public DateTime PasswordCreatedAt { get; set; }
		//public IEnumerable<Password> OldPasswords { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public bool IsRemoved { get; set; }
		public int RoleId { get; set; }
		public ApplicationRole Role { get; set; } = default!;
		//public IEnumerable<PersonalQuestion> PersonalQuestions { get; set; } = default!;
	}
}
