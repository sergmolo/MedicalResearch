using MedicalResearch.Business.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MedicalResearch.Business.Models
{
    public class CommandResult
    {
        private readonly List<CommandError> errors = new();

        public List<CommandError> Errors { get => errors; }

        public bool Succeeded => !errors.Any();

        public CommandResult()
        {
        }

        public CommandResult(CommandErrorCode errorCode)
        {
            errors.Add(new CommandError((int)errorCode, errorCode.ToString()));
        }

        public CommandResult(IEnumerable<CommandErrorCode> errorCodes)
        {
            errors.AddRange(errorCodes.Select(e => new CommandError((int)e, e.ToString())));
        }
    }
}
