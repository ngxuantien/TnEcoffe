using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Domain.Entities.Catalog;

public sealed class ProductImage : BaseEntity
{
    public Guid ProductId { get; private set; }

    public string PublicId { get; private set; } = default!;

    public string Url { get; private set; } = default!;

    public string? AltText { get; private set; }

    public int SortOrder { get; private set; }

    public bool IsPrimary { get; private set; }

    private ProductImage() { }

    internal ProductImage(Guid productId, string publicId, string url, string? altText, bool isPrimary, int sortOrder)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            throw new DomainException(ErrorCodes.Product.ImagePublicIdRequired, "Mã ảnh không được trống.");

        if (string.IsNullOrWhiteSpace(url))
            throw new DomainException(ErrorCodes.Product.ImageUrlRequired, "Đường dẫn ảnh không được trống.");

        ProductId = productId;
        PublicId = publicId.Trim();
        Url = url.Trim();
        AltText = altText?.Trim();
        IsPrimary = isPrimary;
        SortOrder = sortOrder;
    }

    internal void UnsetPrimary() => IsPrimary = false;
}
