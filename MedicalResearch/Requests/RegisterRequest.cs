using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalResearch.Requests
{
	public class RegisterRequest
	{
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string Initials { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string Password { get; set; } = default!;

	}
}
