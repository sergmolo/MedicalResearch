using MedicalResearch.Data.Enums;

namespace MedicalResearch
{
    public class CommandResult
    {
        public int? Code { get; }
        public string? Description { get; }

        public bool Succeeded
        {
            get
            {
                return !Code.HasValue;
            }
        }

        public CommandResult()
        {
        }

        public CommandResult(CommandError result)
        {
            Code = (int)result;
            Description = result.ToString();
        }
    }
}
