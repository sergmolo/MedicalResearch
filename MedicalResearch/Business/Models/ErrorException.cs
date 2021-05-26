using MedicalResearch.Business.Enums;
using System;

namespace MedicalResearch.Business.Models
{
    public class ErrorException : Exception
    {
        public CommandResult CommandResult { get; }

        public ErrorException(CommandErrorCode errorCode)
        {
            CommandResult = CommandResult.Failed(errorCode);
        }
    }
}
