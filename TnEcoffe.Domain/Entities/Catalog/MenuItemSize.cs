using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Domain.Entities.Catalog;

public sealed class MenuItemSize : BaseEntity
{
    public Guid MenuItemId { get; private set; }

    public string Code { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public long Price { get; private set; }

    public long? CostPrice { get; private set; }

    public bool IsDefault { get; private set; }

    public int SortOrder { get; private set; }

    private MenuItemSize() { }

    internal MenuItemSize(Guid menuItemId, string code, string name, long price, bool isDefault, int sortOrder)
    {
        MenuItemId = menuItemId;
        Code = code;
        Name = name.Trim();
        SetPrice(price, costPrice: null);
        IsDefault = isDefault;
        SortOrder = sortOrder;
    }

    public void SetPrice(long price, long? costPrice)
    {
        if (price < 0 || costPrice < 0)
            throw new DomainException(ErrorCodes.MenuItem.PriceNegative, "Giá không được âm.");

        Price = price;
        CostPrice = costPrice;
    }

    internal void UnsetDefault() => IsDefault = false;
}
