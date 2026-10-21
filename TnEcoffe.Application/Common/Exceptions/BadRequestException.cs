using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Application.Common.Exceptions;

public sealed class BadRequestException(string message, string code = ErrorCodes.BadRequest)
    : AppException(ErrorType.BadRequest, code, message);
