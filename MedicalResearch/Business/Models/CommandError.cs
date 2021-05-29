namespace MedicalResearch.Business.Models
{
    public class CommandError
    {
        public string Code { get; init; }
        public string Description { get; init; } = default!;

        public CommandError(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
