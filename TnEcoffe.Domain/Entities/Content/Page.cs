using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;
using TnEcoffe.Domain.Enums.Content;

namespace TnEcoffe.Domain.Entities.Content;

public sealed class Page : AuditableEntity
{
    public PageKind Kind { get; private set; }

    public string Slug { get; private set; } = default!;

    public string Title { get; private set; } = default!;

    public string? Summary { get; private set; }

    public string Content { get; private set; } = default!;

    public int SortOrder { get; private set; }

    public bool IsPublished { get; private set; } = true;

    private Page() { }

    public Page(PageKind kind, string slug, string title, string content)
    {
        Kind = kind;
        Update(slug, title, summary: null, content, sortOrder: 0, isPublished: true);
    }

    public void Update(string slug, string title, string? summary, string content, int sortOrder, bool isPublished)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException(ErrorCodes.Content.PageSlugRequired, "Slug trang không được trống.");

        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException(ErrorCodes.Content.PageTitleRequired, "Tiêu đề không được trống.");

        Slug = slug.Trim().ToLowerInvariant();
        Title = title.Trim();
        Summary = summary?.Trim();
        Content = content;
        SortOrder = sortOrder;
        IsPublished = isPublished;
    }
}
