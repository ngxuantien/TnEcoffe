using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Domain.Entities.Content;

public sealed class GalleryImage : AuditableEntity
{
    public string PublicId { get; private set; } = default!;

    public string Url { get; private set; } = default!;

    public string? Caption { get; private set; }

    public int SortOrder { get; private set; }

    public bool IsActive { get; private set; } = true;

    private GalleryImage() { }

    public GalleryImage(string publicId, string url, string? caption, int sortOrder)
    {
        if (string.IsNullOrWhiteSpace(publicId) || string.IsNullOrWhiteSpace(url))
            throw new DomainException(ErrorCodes.Content.GalleryImageRequired, "Ảnh không hợp lệ.");

        PublicId = publicId.Trim();
        Url = url.Trim();
        Update(caption, sortOrder, isActive: true);
    }

    public void Update(string? caption, int sortOrder, bool isActive)
    {
        Caption = caption?.Trim();
        SortOrder = sortOrder;
        IsActive = isActive;
    }
}
