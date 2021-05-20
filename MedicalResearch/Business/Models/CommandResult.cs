using MedicalResearch.Business.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MedicalResearch.Business.Models
{
    public class CommandResult
    {
        private readonly List<CommandError> _errors = new();

        public List<CommandError> Errors { get => _errors; }

        public bool Succeeded => !_errors.Any();

        public CommandResult()
        {
        }

        public CommandResult(CommandErrorCode errorCode)
        {
            _errors.Add(new CommandError((int)errorCode, errorCode.ToString()));
        }

        public CommandResult(IEnumerable<CommandErrorCode> errorCodes)
        {
            _errors.AddRange(errorCodes.Select(e => new CommandError((int)e, e.ToString())));
        }
    }
}
