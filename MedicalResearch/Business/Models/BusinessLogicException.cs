using System;

namespace MedicalResearch.Business.Models
{
    public class BusinessLogicException : Exception
    {
        public CommandResult CommandResult { get; }

        public bool NotFound { get; set; }

        public BusinessLogicException(CommandResult commandResult)
        {
            CommandResult = commandResult;
        }
    }
}
