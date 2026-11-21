namespace Shared;

/// <summary>Данные правила скидки для отображения.</summary>
public sealed class DiscountDto
{
    /// <summary>
    /// Идентификатор объекта.
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Размер пиццы.
    /// </summary>
    public int Size { get; init; }
    /// <summary>
    /// Процент скидки для указанного размера.
    /// </summary>
    public decimal DiscountPercent { get; init; }
}
