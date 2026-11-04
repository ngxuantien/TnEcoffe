using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;
using TnEcoffe.Domain.Common.Interface;

namespace TnEcoffe.Domain.Entities.Catalog;

public sealed class MenuItem : AuditableEntity, ISoftDeletable
{
    private readonly List<MenuItemSize> _sizes = [];
    private readonly List<MenuItemImage> _images = [];

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string Slug { get; private set; } = default!;

    public string Sku { get; private set; } = default!;

    public string? ShortDescription { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; } = true;        // đang kinh doanh

    public bool IsAvailable { get; private set; } = true;     // còn hàng hôm nay

    public bool IsNew { get; private set; }

    public bool IsFeatured { get; private set; }              // hiện ở trang chủ

    public int? FeaturedOrder { get; private set; }

    public bool SellOnline { get; private set; } = true;

    public bool SellPos { get; private set; } = true;

    public TimeOnly? AvailableFrom { get; private set; }      // null = bán cả ngày

    public TimeOnly? AvailableTo { get; private set; }

    public string AiTags { get; private set; } = "{}";

    public string? AiNote { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public Guid? DeletedBy { get; private set; }

    public IReadOnlyCollection<MenuItemSize> Sizes => _sizes;

    public IReadOnlyCollection<MenuItemImage> Images => _images;

    private MenuItem() { }

    public MenuItem(Guid categoryId, string name, string slug, string sku, string? shortDescription)
    {
        CategoryId = categoryId;
        UpdateInfo(name, slug, sku, shortDescription, description: null);
    }

    public void UpdateInfo(string name, string slug, string sku, string? shortDescription, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(ErrorCodes.MenuItem.NameRequired, "Tên món không được trống.");

        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException(ErrorCodes.MenuItem.SlugRequired, "Slug món không được trống.");

        if (string.IsNullOrWhiteSpace(sku))
            throw new DomainException(ErrorCodes.MenuItem.SkuRequired, "Mã SKU không được trống.");

        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Sku = sku.Trim().ToUpperInvariant();
        ShortDescription = shortDescription?.Trim();
        Description = description;
    }

    public void ChangeCategory(Guid categoryId) => CategoryId = categoryId;

    public void SetAvailableTime(TimeOnly? from, TimeOnly? to)
    {
        if ((from is null) != (to is null) || (from is not null && to <= from))
            throw new DomainException(ErrorCodes.MenuItem.AvailableTimeInvalid, "Khung giờ bán không hợp lệ.");

        AvailableFrom = from;
        AvailableTo = to;
    }

    public void SetStatus(bool isActive, bool isAvailable, bool sellOnline, bool sellPos)
    {
        IsActive = isActive;
        IsAvailable = isAvailable;
        SellOnline = sellOnline;
        SellPos = sellPos;
    }

    public void SetFeatured(bool isFeatured, int? order)
    {
        IsFeatured = isFeatured;
        FeaturedOrder = isFeatured ? order : null;
    }

    public void AddSize(string code, string name, long price, bool isDefault)
    {
        var normalizedCode = code.Trim().ToUpperInvariant();
        if (_sizes.Any(s => s.Code == normalizedCode))
            throw new DomainException(ErrorCodes.MenuItem.SizeDuplicated, $"Size {normalizedCode} đã tồn tại.");

        isDefault = isDefault || _sizes.Count == 0;
        if (isDefault)
            _sizes.ForEach(s => s.UnsetDefault());

        _sizes.Add(new MenuItemSize(Id, normalizedCode, name, price, isDefault, _sizes.Count));
    }

    public void AddImage(string publicId, string url, string? altText, bool isPrimary)
    {
        // Ảnh đầu tiên luôn là ảnh chính
        isPrimary = isPrimary || _images.Count == 0;
        if (isPrimary)
            _images.ForEach(i => i.UnsetPrimary());

        _images.Add(new MenuItemImage(Id, publicId, url, altText, isPrimary, _images.Count));
    }
}