namespace MedicalResearch.V1.Requests
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
