﻿using System.Collections.Generic;

namespace MedicalResearch.Business.Models
{
    public class CommandResult
    {
        private readonly List<object> _errors = new();

        public List<object> Errors { get => _errors; }

        public bool Succeeded { get; }

        private CommandResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public static CommandResult Success()
        {
            return new CommandResult(true);
        }

        public static CommandResult Failed()
        {
            return new CommandResult(false);
        }

        public static CommandResult Failed(IEnumerable<object> errors)
        {
            var res = Failed();
            res._errors.AddRange(errors);
            return res;
        }
    }
}
