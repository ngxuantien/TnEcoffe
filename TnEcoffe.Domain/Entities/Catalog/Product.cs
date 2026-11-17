using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;
using TnEcoffe.Domain.Common.Interface;
using TnEcoffe.Domain.Enums.Catalog;

namespace TnEcoffe.Domain.Entities.Catalog;

public sealed class Product : AuditableEntity, ISoftDeletable
{
    private readonly List<ProductVariant> _variants = [];
    private readonly List<ProductImage> _images = [];

    public ProductKind Kind { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string Slug { get; private set; } = default!;

    public string Sku { get; private set; } = default!;

    public string? ShortDescription { get; private set; }

    public string? Description { get; private set; }          // markdown

    public bool IsActive { get; private set; } = true;        // đang kinh doanh

    public bool IsAvailable { get; private set; } = true;     // đồ uống: còn bán hôm nay

    public bool IsFeatured { get; private set; }              // hiện ở trang chủ

    public int? FeaturedOrder { get; private set; }

    public bool IsNew { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public Guid? DeletedBy { get; private set; }

    public IReadOnlyCollection<ProductVariant> Variants => _variants;

    public IReadOnlyCollection<ProductImage> Images => _images;

    private Product() { }

    public Product(Category category, string name, string slug, string sku, string? shortDescription)
    {
        Kind = category.Kind;   // loại sản phẩm luôn theo danh mục
        CategoryId = category.Id;
        UpdateInfo(name, slug, sku, shortDescription, description: null);
    }

    public void UpdateInfo(string name, string slug, string sku, string? shortDescription, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(ErrorCodes.Product.NameRequired, "Tên sản phẩm không được trống.");

        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException(ErrorCodes.Product.SlugRequired, "Slug sản phẩm không được trống.");

        if (string.IsNullOrWhiteSpace(sku))
            throw new DomainException(ErrorCodes.Product.SkuRequired, "Mã SKU không được trống.");

        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Sku = sku.Trim().ToUpperInvariant();
        ShortDescription = shortDescription?.Trim();
        Description = description;
    }

    public void ChangeCategory(Category category)
    {
        if (category.Kind != Kind)
            throw new DomainException(ErrorCodes.Product.KindMismatch, "Danh mục mới phải cùng loại với sản phẩm.");

        CategoryId = category.Id;
    }

    public void SetStatus(bool isActive, bool isAvailable, bool isNew)
    {
        IsActive = isActive;
        IsAvailable = isAvailable;
        IsNew = isNew;
    }

    public void SetFeatured(bool isFeatured, int? order)
    {
        IsFeatured = isFeatured;
        FeaturedOrder = isFeatured ? order : null;
    }

    public ProductVariant AddVariant(string code, string name, long price, bool isDefault,
        int? initialStock = null, int? weightGrams = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new DomainException(ErrorCodes.Product.VariantCodeRequired, "Mã biến thể không được trống.");

        var normalizedCode = code.Trim().ToUpperInvariant();
        if (_variants.Any(v => v.Code == normalizedCode))
            throw new DomainException(ErrorCodes.Product.VariantDuplicated, $"Biến thể {normalizedCode} đã tồn tại.");

        // Sản phẩm đóng gói bắt buộc đếm tồn kho; đồ uống thì không
        var trackInventory = Kind == ProductKind.Retail;
        if (trackInventory && initialStock is null)
            throw new DomainException(ErrorCodes.Product.StockRequired, "Sản phẩm đóng gói cần số lượng tồn ban đầu.");

        // Biến thể đầu tiên luôn là mặc định
        isDefault = isDefault || _variants.Count == 0;
        if (isDefault)
            _variants.ForEach(v => v.UnsetDefault());

        var variant = new ProductVariant(Id, normalizedCode, name, price, isDefault, _variants.Count,
            trackInventory, trackInventory ? initialStock : null, weightGrams);

        _variants.Add(variant);
        return variant;
    }

    public void AddImage(string publicId, string url, string? altText, bool isPrimary)
    {
        // Ảnh đầu tiên luôn là ảnh chính
        isPrimary = isPrimary || _images.Count == 0;
        if (isPrimary)
            _images.ForEach(i => i.UnsetPrimary());

        _images.Add(new ProductImage(Id, publicId, url, altText, isPrimary, _images.Count));
    }
}
