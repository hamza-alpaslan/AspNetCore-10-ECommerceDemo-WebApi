using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Results
{
    public class DataResult<T>:Result
    {
        public T Data { get; }

        protected DataResult(T data, bool success, string message)
            : base(success, message)
        {
            Data = data;
        }

        public static DataResult<T> Ok(T data, string message = "")
            => new DataResult<T>(data, true, message);

        public static DataResult<T> Fail(string message)
            => new DataResult<T>(default!, false, message);
    }
}
