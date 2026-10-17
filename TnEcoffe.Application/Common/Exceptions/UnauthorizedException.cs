using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Application.Common.Exceptions;

public sealed class UnauthorizedException(string message = "Bạn chưa đăng nhập.")
    : AppException(ErrorType.Unauthorized, ErrorCodes.Unauthorized, message);
