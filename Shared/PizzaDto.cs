namespace Shared;

/// <summary>Данные пиццы, предназначенные для передачи в представление.</summary>
public sealed class PizzaDto
{
    /// <summary>
    /// Идентификатор объекта.
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Название пиццы.
    /// </summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>
    /// Цена пиццы без скидки.
    /// </summary>
    public decimal Price { get; init; }
    /// <summary>
    /// Цена пиццы с учётом скидки.
    /// </summary>
    public decimal DiscountedPrice { get; init; }
    /// <summary>
    /// Признак того, что пицца относится к категории ПП.
    /// </summary>
    public bool IsHealthy { get; init; }
    /// <summary>
    /// Размер пиццы.
    /// </summary>
    public int Size { get; init; }
}
