using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalResearch.Requests
{
	public class EditUserRequest
	{
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string Initials { get; set; } = default!;
		public string? NewPassword { get; set; }
	}
}
