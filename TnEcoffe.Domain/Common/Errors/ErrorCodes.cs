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

    public static class Settings
    {
        public const string KeyRequired = "SETTING_KEY_REQUIRED";
        public const string OpeningHourInvalid = "OPENING_HOUR_INVALID";
        public const string SpecialDayInvalid = "SPECIAL_DAY_INVALID";
    }

    public static class Category
    {
        public const string NameRequired = "CATEGORY_NAME_REQUIRED";
        public const string SlugRequired = "CATEGORY_SLUG_REQUIRED";
        public const string NameDuplicated = "CATEGORY_NAME_DUPLICATED";
        public const string DisplayOrderInvalid = "CATEGORY_DISPLAY_ORDER_INVALID";
        public const string HasMenuItems = "CATEGORY_HAS_MENU_ITEMS";
    }

    public static class MenuItem
    {
        public const string NameRequired = "MENU_ITEM_NAME_REQUIRED";
        public const string SlugRequired = "MENU_ITEM_SLUG_REQUIRED";
        public const string SkuRequired = "MENU_ITEM_SKU_REQUIRED";
        public const string AvailableTimeInvalid = "MENU_ITEM_AVAILABLE_TIME_INVALID";
        public const string SizeDuplicated = "MENU_ITEM_SIZE_DUPLICATED";
        public const string PriceNegative = "MENU_ITEM_PRICE_NEGATIVE";
        public const string ImageUrlRequired = "MENU_ITEM_IMAGE_URL_REQUIRED";
        public const string ImagePublicIdRequired = "MENU_ITEM_IMAGE_PUBLIC_ID_REQUIRED";
    }

    public static class Newsletter
    {
        public const string EmailInvalid = "NEWSLETTER_EMAIL_INVALID";
    }
}
