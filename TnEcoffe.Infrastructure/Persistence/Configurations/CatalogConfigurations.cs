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

        // Chỉ unique trong các dòng chưa xoá, để xoá mềm rồi tạo lại cùng slug vẫn được
        b.HasIndex(x => x.Slug).IsUnique().HasFilter("deleted_at IS NULL");
    }
}

internal sealed class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> b)
    {
        b.ToTable("menu_items", "catalog", t => t.HasCheckConstraint("ck_menu_items_available_time",
            "(available_from IS NULL) = (available_to IS NULL)"));

        b.Property(x => x.Name).HasMaxLength(150);
        b.Property(x => x.Slug).HasMaxLength(170);
        b.Property(x => x.Sku).HasMaxLength(30);
        b.Property(x => x.ShortDescription).HasMaxLength(200);
        b.Property(x => x.AiTags).HasColumnType("jsonb");

        // Mặc định EF để Cascade; xoá danh mục không được kéo theo xoá món nên phải khai báo
        b.HasOne(x => x.Category).WithMany().OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => x.Slug).IsUnique().HasFilter("deleted_at IS NULL");
        b.HasIndex(x => x.Sku).IsUnique().HasFilter("deleted_at IS NULL");
        b.HasIndex(x => x.FeaturedOrder).HasDatabaseName("ix_menu_items_featured").HasFilter("is_featured AND deleted_at IS NULL");
    }
}

internal sealed class MenuItemSizeConfiguration : IEntityTypeConfiguration<MenuItemSize>
{
    public void Configure(EntityTypeBuilder<MenuItemSize> b)
    {
        b.ToTable("menu_item_sizes", "catalog", t =>
        {
            t.HasCheckConstraint("ck_menu_item_sizes_price", "price >= 0");
            t.HasCheckConstraint("ck_menu_item_sizes_cost_price", "cost_price IS NULL OR cost_price >= 0");
        });

        b.Property(x => x.Code).HasMaxLength(10);
        b.Property(x => x.Name).HasMaxLength(30);

        b.HasIndex(x => new { x.MenuItemId, x.Code }).IsUnique();

        // Mỗi món tối đa 1 size mặc định
        b.HasIndex(x => x.MenuItemId).HasDatabaseName("ux_menu_item_sizes_default").IsUnique().HasFilter("is_default");
    }
}

internal sealed class MenuItemImageConfiguration : IEntityTypeConfiguration<MenuItemImage>
{
    public void Configure(EntityTypeBuilder<MenuItemImage> b)
    {
        b.ToTable("menu_item_images", "catalog");

        b.Property(x => x.PublicId).HasMaxLength(255);
        b.Property(x => x.AltText).HasMaxLength(150);

        // Index thường để lấy ảnh theo món; index có filter chỉ phủ các dòng is_primary
        b.HasIndex(x => x.MenuItemId, "ix_menu_item_images_menu_item_id");
        b.HasIndex(x => x.MenuItemId, "ux_menu_item_images_primary").HasDatabaseName("ux_menu_item_images_primary").IsUnique().HasFilter("is_primary");
    }
}
