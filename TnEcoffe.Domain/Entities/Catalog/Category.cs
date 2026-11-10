using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;
using TnEcoffe.Domain.Common.Interface;

namespace TnEcoffe.Domain.Entities.Catalog;

public sealed class Category : AuditableEntity, ISoftDeletable
{
    public string Name { get; private set; } = default!;

    public string Slug { get; private set; } = default!;

    public string? Description { get; private set; }

    public int SortOrder { get; private set; }

    public bool IsVisible { get; private set; } = true;

    public DateTime? DeletedAt { get; private set; }

    public Guid? DeletedBy { get; private set; }

    private Category() { }

    public Category(string name, string slug, string? description, int sortOrder)
    {
        Update(name, slug, description, sortOrder, isVisible: true);
    }

    public void Update(string name, string slug, string? description, int sortOrder, bool isVisible)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(ErrorCodes.Category.NameRequired, "Tên danh mục không được trống.");

        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException(ErrorCodes.Category.SlugRequired, "Slug danh mục không được trống.");

        if (sortOrder < 0)
            throw new DomainException(ErrorCodes.Category.DisplayOrderInvalid, "Thứ tự hiển thị không được âm.");

        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Description = description?.Trim();
        SortOrder = sortOrder;
        IsVisible = isVisible;
    }
}
