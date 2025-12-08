using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Results
{
    public class ErrorResult:Result
    {
        public ErrorType Type { get; }

        public ErrorResult(string message, ErrorType type)
            : base(false, message)
        {
            Type = type;
        }

        public static ErrorResult NotFound(string message) =>
            new ErrorResult(message, ErrorType.NotFound);

        public static ErrorResult Validation(string message) =>
            new ErrorResult(message, ErrorType.Validation);

        public static ErrorResult Business(string message) =>
            new ErrorResult(message, ErrorType.Business);
    }
}
