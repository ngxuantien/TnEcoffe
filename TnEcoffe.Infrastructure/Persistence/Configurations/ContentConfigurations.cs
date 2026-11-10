using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TnEcoffe.Domain.Entities.Content;

namespace TnEcoffe.Infrastructure.Persistence.Configurations;

internal sealed class NewsletterSubscriberConfiguration : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> b)
    {
        b.ToTable("newsletter_subscribers", "content");

        b.Property(x => x.Email).HasMaxLength(254);
        b.Property(x => x.Source).HasMaxLength(30);

        // Entity đã chuẩn hoá email về chữ thường nên unique index thường là đủ
        b.HasIndex(x => x.Email).IsUnique();
    }
}
