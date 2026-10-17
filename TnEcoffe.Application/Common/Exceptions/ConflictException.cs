using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Application.Common.Exceptions;

public sealed class ConflictException(string message, string code = ErrorCodes.Conflict)
: AppException(ErrorType.Conflict, code, message);
