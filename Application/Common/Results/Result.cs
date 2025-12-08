using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Results
{
    public class Result:IResult
    {
        public bool Success { get; }
        public string Message { get; }

        protected Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static Result Ok(string message = "")
            => new Result(true, message);

        public static Result Fail(string message)
            => new Result(false, message);
    }
}
