using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Application.Common.Exceptions;

public sealed class ForbiddenException(string message = "Bạn không có quyền thực hiện thao tác này.")
    : AppException(ErrorType.Forbidden, ErrorCodes.Forbidden, message);