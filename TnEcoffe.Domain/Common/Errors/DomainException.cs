using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Domain.Common.Errors;

public sealed class DomainException(string code, string message) 
    : AppException(ErrorType.BusinessRule, code, message);
