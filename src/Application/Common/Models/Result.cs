using System;
using System.Collections.Generic;
using System.Linq;

namespace HPC.Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success() => new (succeeded: true, errors: Array.Empty<string>());

        public static Result Failure(IEnumerable<string> errors) => new (succeeded: false, errors: errors);
    }
}
