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
        public const string HasProducts = "CATEGORY_HAS_PRODUCTS";
    }

    public static class Product
    {
        public const string NameRequired = "PRODUCT_NAME_REQUIRED";
        public const string SlugRequired = "PRODUCT_SLUG_REQUIRED";
        public const string SkuRequired = "PRODUCT_SKU_REQUIRED";
        public const string KindMismatch = "PRODUCT_KIND_MISMATCH";
        public const string VariantCodeRequired = "PRODUCT_VARIANT_CODE_REQUIRED";
        public const string VariantNameRequired = "PRODUCT_VARIANT_NAME_REQUIRED";
        public const string VariantDuplicated = "PRODUCT_VARIANT_DUPLICATED";
        public const string PriceNegative = "PRODUCT_PRICE_NEGATIVE";
        public const string ComparePriceInvalid = "PRODUCT_COMPARE_PRICE_INVALID";
        public const string StockRequired = "PRODUCT_STOCK_REQUIRED";
        public const string StockNegative = "PRODUCT_STOCK_NEGATIVE";
        public const string WeightInvalid = "PRODUCT_WEIGHT_INVALID";
        public const string ImageUrlRequired = "PRODUCT_IMAGE_URL_REQUIRED";
        public const string ImagePublicIdRequired = "PRODUCT_IMAGE_PUBLIC_ID_REQUIRED";
    }

    public static class Content
    {
        public const string PageTitleRequired = "PAGE_TITLE_REQUIRED";
        public const string PageSlugRequired = "PAGE_SLUG_REQUIRED";
        public const string GalleryImageRequired = "GALLERY_IMAGE_REQUIRED";
    }

    public static class Newsletter
    {
        public const string EmailInvalid = "NEWSLETTER_EMAIL_INVALID";
    }
}
