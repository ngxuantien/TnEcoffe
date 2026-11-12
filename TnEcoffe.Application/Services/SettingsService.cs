using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TnEcoffe.Application.Abstractions.Repositories;
using TnEcoffe.Application.Common.Exceptions;
using TnEcoffe.Application.Dtos.Settings;
using TnEcoffe.Application.Interfaces;
using TnEcoffe.Domain.Entities.Settings;

namespace TnEcoffe.Application.Services;

internal sealed class SettingsService(
    IReadRepository<StoreSetting> settings,
    IReadRepository<OpeningHour> openingHours) : ISettingsService
{
    private const string StoreInfoKey = "store_info";

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<StoreInfoDto> GetStoreInfoAsync(CancellationToken ct = default)
    {
        var json = await settings.Query()
            .Where(x => x.Key == StoreInfoKey)
            .Select(x => x.Value)
            .FirstOrDefaultAsync(ct)
            ?? throw new NotFoundException("Chưa cấu hình thông tin quán.");

        return JsonSerializer.Deserialize<StoreInfoDto>(json, JsonOptions)!;
    }

    public async Task<IReadOnlyList<OpeningHourDto>> GetOpeningHoursAsync(CancellationToken ct = default)
    {
        var rows = await openingHours.Query().ToListAsync(ct);

        return rows
            .OrderBy(x => ((int)x.DayOfWeek + 6) % 7)
            .Select(x => new OpeningHourDto(
                (int)x.DayOfWeek,
                x.IsOpen,
                x.OpenTime?.ToString("HH:mm"),
                x.CloseTime?.ToString("HH:mm")))
            .ToList();
    }
}