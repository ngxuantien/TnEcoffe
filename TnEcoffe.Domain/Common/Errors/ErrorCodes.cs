using System;
using System.Collections.Generic;
using System.Text;

namespace TnEcoffe.Domain.Common.Errors;

public static class ErrorCodes
{
    public const string Validation = "VALIDATION_ERROR";
    public const string BadRequest = "BAD_REQUEST";
    public const string BusinessRule = "BUSINESS_RULE_VIOLATION";
    public const string NotFound = "NOT_FOUND";
    public const string Conflict = "CONFLICT";
    public const string Unauthorized = "UNAUTHORIZED";
    public const string Forbidden = "FORBIDDEN";
    public const string Internal = "INTERNAL_ERROR";
    public const string ExternalServiceError = "EXTERNAL_SERVICE_ERROR";
    public const string ExternalServiceTimeout = "EXTERNAL_SERVICE_TIMEOUT";

    public static class Category
    {
        public const string NameRequired = "CATEGORY_NAME_REQUIRED";
        public const string NameDuplicated = "CATEGORY_NAME_DUPLICATED";
        public const string DisplayOrderInvalid = "CATEGORY_DISPLAY_ORDER_INVALID";
    }
}
