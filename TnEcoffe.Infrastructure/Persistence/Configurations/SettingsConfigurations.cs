using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TnEcoffe.Domain.Entities.Settings;

namespace TnEcoffe.Infrastructure.Persistence.Configurations;

internal sealed class StoreSettingConfiguration : IEntityTypeConfiguration<StoreSetting>
{
    public void Configure(EntityTypeBuilder<StoreSetting> b)
    {
        b.ToTable("store_settings", "system");

        b.Property(x => x.Key).HasMaxLength(50);
        b.Property(x => x.Value).HasColumnType("jsonb");

        b.HasIndex(x => x.Key).IsUnique();

        b.HasData(new
        {
            Id = Guid.Parse("019a3000-0000-7000-8000-000000000001"),
            Key = "store_info",
            Value = """
                {"name":"[Tên quán]","slogan":"[Slogan]","address":"[Địa chỉ]","phone":"[SĐT]",
                 "email":"[Email]","mapUrl":null,"social":{"facebook":null,"instagram":null,"zalo":null}}
                """,
            CreatedAt = new DateTime(2026, 10, 30, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}

internal sealed class OpeningHourConfiguration : IEntityTypeConfiguration<OpeningHour>
{
    public void Configure(EntityTypeBuilder<OpeningHour> b)
    {
        b.ToTable("opening_hours", "system", t =>
        {
            t.HasCheckConstraint("ck_opening_hours_day", "day_of_week BETWEEN 0 AND 6");
            t.HasCheckConstraint("ck_opening_hours_time",
                "NOT is_open OR (open_time IS NOT NULL AND close_time IS NOT NULL AND close_time > open_time)");
        });

        b.Property(x => x.DayOfWeek).HasConversion<short>();

        b.HasIndex(x => x.DayOfWeek).IsUnique();

        var open = new TimeOnly(6, 0);
        var close = new TimeOnly(22, 0);

        b.HasData(Enumerable.Range(0, 7).Select(d => new
        {
            Id = Guid.Parse($"019a3000-0000-7000-8000-00000000010{d}"),
            DayOfWeek = (DayOfWeek)d,
            IsOpen = true,
            OpenTime = (TimeOnly?)open,
            CloseTime = (TimeOnly?)close
        }));
    }
}

internal sealed class SpecialDayConfiguration : IEntityTypeConfiguration<SpecialDay>
{
    public void Configure(EntityTypeBuilder<SpecialDay> b)
    {
        b.ToTable("special_days", "system", t => t.HasCheckConstraint("ck_special_days_time",
            "is_closed OR (open_time IS NOT NULL AND close_time IS NOT NULL AND close_time > open_time)"));

        b.Property(x => x.Note).HasMaxLength(200);

        b.HasIndex(x => x.Date).IsUnique();
    }
}
