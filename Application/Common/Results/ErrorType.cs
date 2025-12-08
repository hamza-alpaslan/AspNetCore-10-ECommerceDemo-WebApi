using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Results
{
    public enum ErrorType
    {
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Business
    }
}
