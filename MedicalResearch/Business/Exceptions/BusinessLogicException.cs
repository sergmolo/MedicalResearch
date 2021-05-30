using MedicalResearch.Business.Pipeline;
using Microsoft.AspNetCore.Http;
using System;

namespace MedicalResearch.Business.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public CommandResult CommandResult { get; }

        public int StatusCode { get; }

        public BusinessLogicException(CommandResult commandResult)
        {
            CommandResult = commandResult;
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public BusinessLogicException(CommandResult commandResult, int statusCode) : this(commandResult)
        {
            StatusCode = statusCode;
        }
    }
}
