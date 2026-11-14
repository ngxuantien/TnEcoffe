namespace TnEcoffe.Application.Dtos.Settings;

public sealed record StoreInfoDto(
    string Name,
    string? Slogan,
    string? Address,
    string? Phone,
    string? Email,
    string? MapUrl,
    SocialLinksDto Social);

public sealed record SocialLinksDto(string? Facebook, string? Instagram, string? Zalo);