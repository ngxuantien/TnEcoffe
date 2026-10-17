using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Application.Common.Exceptions;

public sealed class ValidationException : AppException
{
    public ValidationException(IDictionary<string, string[]> errors)
        : base(ErrorType.Validation, ErrorCodes.Validation, "Dữ liệu không hợp lệ.")
    {
        Errors = new Dictionary<string, string[]>(errors);
    }

    public ValidationException(string field, string error)
        : this(new Dictionary<string, string[]> { [field] = [error] })
    {
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
