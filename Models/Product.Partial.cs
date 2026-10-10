namespace Lab3.Models;

public partial class Product
{
    /// <summary>Наименование товара (из справочника ProductNames).</summary>
    public string? Name => ProductName?.Name;

    /// <summary>Действующая скидка, %.</summary>
    public double CurrentDiscount => Discount ?? 0;

    /// <summary>Итоговая цена с учётом действующей скидки.</summary>
    public double? FinalPrice => Price.HasValue ? Price.Value * (1 - (Discount ?? 0) / 100.0) : null;

    /// <summary>Цена снижена (скидка действует).</summary>
    public bool HasDiscount => (Discount ?? 0) > 0;

    /// <summary>Скидка превышает 20% (подсветка строки).</summary>
    public bool HasBigDiscount => (Discount ?? 0) > 20;

    /// <summary>Товар отсутствует на складе.</summary>
    public bool IsOutOfStock => (Count ?? 0) <= 0;
}