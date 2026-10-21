using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Domain.Common.Errors;

public abstract class AppException(ErrorType type, string code, string message) : Exception(message)
{
    public ErrorType Type { get; } = type;

    public string Code { get; } = code;
}
