namespace MedicalResearch.Business.Models
{
    public class CommandError
    {
        public int Code { get; init; }
        public string Description { get; init; } = default!;

        public CommandError(int code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
