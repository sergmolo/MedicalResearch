using MedicalResearch.Enums;
using MedicalResearch.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalResearch.Responses
{
	public class UserResponse
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string Initials { get; set; } = default!;
		public string Email { get; set; } = default!;
		public Role Role { get; set; } = default!;
	}
}
