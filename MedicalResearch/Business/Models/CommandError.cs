namespace MedicalResearch.Business.Models
{
    public class CommandError
    {
        public int Code { get; set; }
        public string Description { get; set; } = default!;

        public CommandError(int code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
