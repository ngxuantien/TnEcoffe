using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TnEcoffe.Domain.Entities.Catalog;

namespace TnEcoffe.Infrastructure.Persistence.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> b)
    {
        b.ToTable("categories", "catalog");

        b.Property(x => x.Name).HasMaxLength(100);
        b.Property(x => x.Slug).HasMaxLength(120);
        b.Property(x => x.Description).HasMaxLength(300);

        b.HasIndex(x => x.Slug).IsUnique().HasFilter("deleted_at IS NULL");
    }
}

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> b)
    {
        b.ToTable("products", "catalog");

        b.Property(x => x.Name).HasMaxLength(150);
        b.Property(x => x.Slug).HasMaxLength(170);
        b.Property(x => x.Sku).HasMaxLength(30);
        b.Property(x => x.ShortDescription).HasMaxLength(200);

        b.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        b.HasMany(x => x.Variants).WithOne().HasForeignKey(v => v.ProductId).OnDelete(DeleteBehavior.Cascade);
        b.HasMany(x => x.Images).WithOne().HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => x.Slug).IsUnique().HasFilter("deleted_at IS NULL");
        b.HasIndex(x => x.Sku).IsUnique().HasFilter("deleted_at IS NULL");

        b.HasIndex(x => new { x.Kind, x.CategoryId })
           .HasDatabaseName("ix_products_listing")
           .HasFilter("is_active AND deleted_at IS NULL");

        b.HasIndex(x => new { x.Kind, x.FeaturedOrder })
            .HasDatabaseName("ix_products_featured")
            .HasFilter("is_featured AND deleted_at IS NULL");
    }
}

internal sealed class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> b)
    {
        b.ToTable("product_variants", "catalog", t =>
        {
            t.HasCheckConstraint("ck_product_variants_price", "price >= 0");
            t.HasCheckConstraint("ck_product_variants_compare_price", "compare_at_price IS NULL OR compare_at_price > price");
            t.HasCheckConstraint("ck_product_variants_cost_price", "cost_price IS NULL OR cost_price >= 0");
            t.HasCheckConstraint("ck_product_variants_stock", "stock_quantity IS NULL OR stock_quantity >= 0");
            t.HasCheckConstraint("ck_product_variants_tracking", "track_inventory = (stock_quantity IS NOT NULL)");
            t.HasCheckConstraint("ck_product_variants_weight", "weight_grams IS NULL OR weight_grams > 0");
        });

        b.Property(x => x.Code).HasMaxLength(20);
        b.Property(x => x.Name).HasMaxLength(50);

        b.Property(x => x.Version).IsRowVersion();

        b.HasIndex(x => new { x.ProductId, x.Code }).IsUnique();

        b.HasIndex(x => x.ProductId, "ux_product_variants_default")
            .HasDatabaseName("ux_product_variants_default")
            .IsUnique()
            .HasFilter("is_default");
    }
}

internal sealed class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> b)
    {
        b.ToTable("product_images", "catalog");

        b.Property(x => x.PublicId).HasMaxLength(200);
        b.Property(x => x.AltText).HasMaxLength(150);

        b.HasIndex(x => x.ProductId, "ix_product_images_product_id")
            .HasDatabaseName("ix_product_images_product_id");

        b.HasIndex(x => x.ProductId, "ux_product_images_primary")
            .HasDatabaseName("ux_product_images_primary")
            .IsUnique()
            .HasFilter("is_primary");
    }
}