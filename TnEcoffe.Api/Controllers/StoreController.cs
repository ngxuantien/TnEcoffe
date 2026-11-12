using Microsoft.AspNetCore.Mvc;
using TnEcoffe.Api.Common;
using TnEcoffe.Application.Dtos.Settings;
using TnEcoffe.Application.Interfaces;

namespace TnEcoffe.Api.Controllers;

[ApiController]
[Route("api")]
public sealed class StoreController(ISettingsService settings) : ControllerBase
{
    [HttpGet("store-info")]
    public async Task<BaseResponse<StoreInfoDto>> GetStoreInfo(CancellationToken ct)
        => BaseResponse<StoreInfoDto>.Ok(await settings.GetStoreInfoAsync(ct));

    [HttpGet("opening-hours")]
    public async Task<BaseResponse<IReadOnlyList<OpeningHourDto>>> GetOpeningHours(CancellationToken ct)
        => BaseResponse<IReadOnlyList<OpeningHourDto>>.Ok(await settings.GetOpeningHoursAsync(ct));
}