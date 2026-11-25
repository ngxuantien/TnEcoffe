using System;
using System.Collections.Generic;
using System.Text;
using TnEcoffe.Domain.Common;
using TnEcoffe.Domain.Common.Errors;

namespace TnEcoffe.Domain.Entities.Catalog;

public sealed class ProductVariant : BaseEntity
{
    public Guid ProductId { get; private set; }

    public string Code { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public long Price { get; private set; }

    public long? CompareAtPrice { get; private set; }

    public long? CostPrice { get; private set; }

    public bool TrackInventory { get; private set; }

    public int? StockQuantity { get; private set; }

    public int? WeightGrams { get; private set; }

    public bool IsDefault { get; private set; }

    public bool IsActive { get; private set; } = true;

    public int SortOrder { get; private set; }

    public uint Version { get; private set; }

    private ProductVariant() { }

    internal ProductVariant(Guid productId, string code, string name, long price, bool isDefault, int sortOrder,
        bool trackInventory, int? stockQuantity, int? weightGrams)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(ErrorCodes.Product.VariantNameRequired, "Tên biến thể không được trống.");

        if (stockQuantity < 0)
            throw new DomainException(ErrorCodes.Product.StockNegative, "Tồn kho không được âm.");

        if (weightGrams <= 0)
            throw new DomainException(ErrorCodes.Product.WeightInvalid, "Trọng lượng phải lớn hơn 0.");

        ProductId = productId;
        Code = code;
        Name = name.Trim();
        SetPrice(price, compareAtPrice: null, costPrice: null);
        IsDefault = isDefault;
        SortOrder = sortOrder;
        TrackInventory = trackInventory;
        StockQuantity = stockQuantity;
        WeightGrams = weightGrams;
    }

    public void SetPrice(long price, long? compareAtPrice, long? costPrice)
    {
        if (price < 0 || costPrice < 0)
            throw new DomainException(ErrorCodes.Product.PriceNegative, "Giá không được âm.");

        if (compareAtPrice is not null && compareAtPrice <= price)
            throw new DomainException(ErrorCodes.Product.ComparePriceInvalid, "Giá gạch ngang phải lớn hơn giá bán.");

        Price = price;
        CompareAtPrice = compareAtPrice;
        CostPrice = costPrice;
    }

    public void SetActive(bool isActive) => IsActive = isActive;

    public bool IsInStock(int quantity) => !TrackInventory || StockQuantity >= quantity;

    internal void UnsetDefault() => IsDefault = false;
}
