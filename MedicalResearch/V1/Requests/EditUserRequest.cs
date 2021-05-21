namespace MedicalResearch.V1.Requests
{
    public class EditUserRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Initials { get; set; } = default!;
        public string? NewPassword { get; set; }
    }
}
