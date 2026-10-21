using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Application.Common.Exceptions;

public sealed class NotFoundException : AppException
{
    public NotFoundException(string message, string code = ErrorCodes.NotFound)
        : base(ErrorType.NotFound, code, message)
    {
    }

    public NotFoundException(string entityName, object key)
        : this($"Không tìm thấy {entityName} với khóa '{key}'.")
    {
    }
}
