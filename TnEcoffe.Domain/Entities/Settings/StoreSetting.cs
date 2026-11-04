using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Domain.Entities.Settings;

public sealed class StoreSetting : AuditableEntity
{
    public string Key { get; private set; } = default!;

    public string Value { get; private set; } = "{}";

    private StoreSetting() { }

    public StoreSetting(string key, string jsonValue)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new DomainException(ErrorCodes.Settings.KeyRequired, "Khoá cấu hình không được trống.");

        Key = key.Trim();
        Value = jsonValue;
    }

    public void SetValue(string jsonValue) => Value = jsonValue;
}
