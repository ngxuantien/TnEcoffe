using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TnEcoffe.Domain.Entities.Content;

namespace TnEcoffe.Infrastructure.Persistence.Configurations;

internal sealed class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> b)
    {
        b.ToTable("pages", "content");

        b.Property(x => x.Slug).HasMaxLength(100);
        b.Property(x => x.Title).HasMaxLength(150);
        b.Property(x => x.Summary).HasMaxLength(300);

        b.HasIndex(x => x.Slug).IsUnique();
    }
}

internal sealed class GalleryImageConfiguration : IEntityTypeConfiguration<GalleryImage>
{
    public void Configure(EntityTypeBuilder<GalleryImage> b)
    {
        b.ToTable("gallery_images", "content");

        b.Property(x => x.PublicId).HasMaxLength(200);
        b.Property(x => x.Caption).HasMaxLength(150);
    }
}

internal sealed class NewsletterSubscriberConfiguration : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> b)
    {
        b.ToTable("newsletter_subscribers", "content");

        b.Property(x => x.Email).HasMaxLength(254);
        b.Property(x => x.Source).HasMaxLength(30);

        b.HasIndex(x => x.Email).IsUnique();
    }
}