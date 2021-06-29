using MedicalResearch.Data.Enums;

namespace MedicalResearch.V1.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Initials { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Role Role { get; set; }
        public int? ClinicId { get; set; }
    }
}
