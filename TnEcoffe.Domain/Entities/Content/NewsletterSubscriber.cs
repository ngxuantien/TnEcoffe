using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Domain.Entities.Content;

public sealed class NewsletterSubscriber : BaseEntity
{
    public string Email { get; private set; } = default!;

    public string Source { get; private set; } = "footer";

    public DateTime SubscribedAt { get; private set; }

    public DateTime? UnsubscribedAt { get; private set; }

    private NewsletterSubscriber() { }

    public NewsletterSubscriber(string email, string source, DateTime now)
    {
        var normalized = email.Trim().ToLowerInvariant();
        if (normalized.Length > 254 || !normalized.Contains('@'))
            throw new DomainException(ErrorCodes.Newsletter.EmailInvalid, "Email không hợp lệ.");

        Email = normalized;
        Source = source;
        SubscribedAt = now;
    }

    public void Resubscribe(DateTime now)
    {
        SubscribedAt = now;
        UnsubscribedAt = null;
    }

    public void Unsubscribe(DateTime now) => UnsubscribedAt = now;
}
