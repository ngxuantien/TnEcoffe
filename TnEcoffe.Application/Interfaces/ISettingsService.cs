using TnEcoffe.Application.Dtos.Settings;

namespace TnEcoffe.Application.Interfaces;

public interface ISettingsService
{
    Task<StoreInfoDto> GetStoreInfoAsync(CancellationToken ct = default);

    Task<IReadOnlyList<OpeningHourDto>> GetOpeningHoursAsync(CancellationToken ct = default);
}