using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TnEcoffe.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialSettingsAndCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.EnsureSchema(
                name: "content");

            migrationBuilder.EnsureSchema(
                name: "system");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    is_visible = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "newsletter_subscribers",
                schema: "content",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    source = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    subscribed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    unsubscribed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_newsletter_subscribers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "opening_hours",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_of_week = table.Column<short>(type: "smallint", nullable: false),
                    is_open = table.Column<bool>(type: "boolean", nullable: false),
                    open_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    close_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_opening_hours", x => x.id);
                    table.CheckConstraint("ck_opening_hours_day", "day_of_week BETWEEN 0 AND 6");
                    table.CheckConstraint("ck_opening_hours_time", "NOT is_open OR (open_time IS NOT NULL AND close_time IS NOT NULL AND close_time > open_time)");
                });

            migrationBuilder.CreateTable(
                name: "special_days",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_closed = table.Column<bool>(type: "boolean", nullable: false),
                    open_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    close_time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    note = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_special_days", x => x.id);
                    table.CheckConstraint("ck_special_days_time", "is_closed OR (open_time IS NOT NULL AND close_time IS NOT NULL AND close_time > open_time)");
                });

            migrationBuilder.CreateTable(
                name: "store_settings",
                schema: "system",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    value = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_store_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_items",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    slug = table.Column<string>(type: "character varying(170)", maxLength: 170, nullable: false),
                    sku = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    short_description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false),
                    is_new = table.Column<bool>(type: "boolean", nullable: false),
                    is_featured = table.Column<bool>(type: "boolean", nullable: false),
                    featured_order = table.Column<int>(type: "integer", nullable: true),
                    sell_online = table.Column<bool>(type: "boolean", nullable: false),
                    sell_pos = table.Column<bool>(type: "boolean", nullable: false),
                    available_from = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    available_to = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    ai_tags = table.Column<string>(type: "jsonb", nullable: false),
                    ai_note = table.Column<string>(type: "text", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_items", x => x.id);
                    table.CheckConstraint("ck_menu_items_available_time", "(available_from IS NULL) = (available_to IS NULL)");
                    table.ForeignKey(
                        name: "fk_menu_items_categories_category_id",
                        column: x => x.category_id,
                        principalSchema: "catalog",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "menu_item_images",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    menu_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    public_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    alt_text = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_item_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_item_images_menu_items_menu_item_id",
                        column: x => x.menu_item_id,
                        principalSchema: "catalog",
                        principalTable: "menu_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menu_item_sizes",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    menu_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    cost_price = table.Column<long>(type: "bigint", nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_item_sizes", x => x.id);
                    table.CheckConstraint("ck_menu_item_sizes_cost_price", "cost_price IS NULL OR cost_price >= 0");
                    table.CheckConstraint("ck_menu_item_sizes_price", "price >= 0");
                    table.ForeignKey(
                        name: "fk_menu_item_sizes_menu_items_menu_item_id",
                        column: x => x.menu_item_id,
                        principalSchema: "catalog",
                        principalTable: "menu_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "system",
                table: "opening_hours",
                columns: new[] { "id", "close_time", "day_of_week", "is_open", "open_time" },
                values: new object[,]
                {
                    { new Guid("019a3000-0000-7000-8000-000000000100"), new TimeOnly(22, 0, 0), (short)0, true, new TimeOnly(6, 0, 0) },
                    { new Guid("019a3000-0000-7000-8000-000000000101"), new TimeOnly(22, 0, 0), (short)1, true, new TimeOnly(6, 0, 0) },
                    { new Guid("019a3000-0000-7000-8000-000000000102"), new TimeOnly(22, 0, 0), (short)2, true, new TimeOnly(6, 0, 0) },
                    { new Guid("019a3000-0000-7000-8000-000000000103"), new TimeOnly(22, 0, 0), (short)3, true, new TimeOnly(6, 0, 0) },
                    { new Guid("019a3000-0000-7000-8000-000000000104"), new TimeOnly(22, 0, 0), (short)4, true, new TimeOnly(6, 0, 0) },
                    { new Guid("019a3000-0000-7000-8000-000000000105"), new TimeOnly(22, 0, 0), (short)5, true, new TimeOnly(6, 0, 0) },
                    { new Guid("019a3000-0000-7000-8000-000000000106"), new TimeOnly(22, 0, 0), (short)6, true, new TimeOnly(6, 0, 0) }
                });

            migrationBuilder.InsertData(
                schema: "system",
                table: "store_settings",
                columns: new[] { "id", "created_at", "created_by", "key", "updated_at", "updated_by", "value" },
                values: new object[] { new Guid("019a3000-0000-7000-8000-000000000001"), new DateTime(2026, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), null, "store_info", null, null, "{\"name\":\"[Tên quán]\",\"slogan\":\"[Slogan]\",\"address\":\"[Địa chỉ]\",\"phone\":\"[SĐT]\",\n \"email\":\"[Email]\",\"mapUrl\":null,\"social\":{\"facebook\":null,\"instagram\":null,\"zalo\":null}}" });

            migrationBuilder.CreateIndex(
                name: "ix_categories_slug",
                schema: "catalog",
                table: "categories",
                column: "slug",
                unique: true,
                filter: "deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_menu_item_images_menu_item_id",
                schema: "catalog",
                table: "menu_item_images",
                column: "menu_item_id");

            migrationBuilder.CreateIndex(
                name: "ux_menu_item_images_primary",
                schema: "catalog",
                table: "menu_item_images",
                column: "menu_item_id",
                unique: true,
                filter: "is_primary");

            migrationBuilder.CreateIndex(
                name: "ix_menu_item_sizes_menu_item_id_code",
                schema: "catalog",
                table: "menu_item_sizes",
                columns: new[] { "menu_item_id", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_menu_item_sizes_default",
                schema: "catalog",
                table: "menu_item_sizes",
                column: "menu_item_id",
                unique: true,
                filter: "is_default");

            migrationBuilder.CreateIndex(
                name: "ix_menu_items_category_id",
                schema: "catalog",
                table: "menu_items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_items_featured",
                schema: "catalog",
                table: "menu_items",
                column: "featured_order",
                filter: "is_featured AND deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_menu_items_sku",
                schema: "catalog",
                table: "menu_items",
                column: "sku",
                unique: true,
                filter: "deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_menu_items_slug",
                schema: "catalog",
                table: "menu_items",
                column: "slug",
                unique: true,
                filter: "deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_newsletter_subscribers_email",
                schema: "content",
                table: "newsletter_subscribers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_opening_hours_day_of_week",
                schema: "system",
                table: "opening_hours",
                column: "day_of_week",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_special_days_date",
                schema: "system",
                table: "special_days",
                column: "date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_store_settings_key",
                schema: "system",
                table: "store_settings",
                column: "key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_item_images",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "menu_item_sizes",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "newsletter_subscribers",
                schema: "content");

            migrationBuilder.DropTable(
                name: "opening_hours",
                schema: "system");

            migrationBuilder.DropTable(
                name: "special_days",
                schema: "system");

            migrationBuilder.DropTable(
                name: "store_settings",
                schema: "system");

            migrationBuilder.DropTable(
                name: "menu_items",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "catalog");
        }
    }
}
