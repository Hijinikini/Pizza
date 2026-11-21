namespace Shared;

/// <summary>
/// Представляет тип PizzaInputEventArgs, используемый в приложении.
/// </summary>
public sealed class PizzaInputEventArgs : EventArgs
{
    /// <summary>
    /// Идентификатор объекта.
    /// </summary>
    public int Id { get; }
    /// <summary>
    /// Название пиццы.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Цена пиццы без скидки.
    /// </summary>
    public decimal Price { get; }
    /// <summary>
    /// Признак того, что пицца относится к категории ПП.
    /// </summary>
    public bool IsHealthy { get; }
    /// <summary>
    /// Размер пиццы.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Выполняет операцию PizzaInputEventArgs в рамках данного компонента.
    /// </summary>
    public PizzaInputEventArgs(int id, string name, decimal price, bool isHealthy, int size)
    { Id = id; Name = name; Price = price; IsHealthy = isHealthy; Size = size; }
}

/// <summary>
/// Представляет тип IdEventArgs, используемый в приложении.
/// </summary>
public sealed class IdEventArgs : EventArgs
{
    /// <summary>
    /// Идентификатор объекта.
    /// </summary>
    public int Id { get; }
    /// <summary>
    /// Выполняет операцию IdEventArgs в рамках данного компонента.
    /// </summary>
    public IdEventArgs(int id) => Id = id;
}

/// <summary>
/// Представляет тип PriceRangeEventArgs, используемый в приложении.
/// </summary>
public sealed class PriceRangeEventArgs : EventArgs
{
    /// <summary>
    /// Минимальная граница диапазона цен.
    /// </summary>
    public decimal MinPrice { get; }
    /// <summary>
    /// Максимальная граница диапазона цен.
    /// </summary>
    public decimal MaxPrice { get; }
    /// <summary>
    /// Выполняет операцию PriceRangeEventArgs в рамках данного компонента.
    /// </summary>
    public PriceRangeEventArgs(decimal minPrice, decimal maxPrice)
    { MinPrice = minPrice; MaxPrice = maxPrice; }
}

/// <summary>
/// Представляет тип DiscountInputEventArgs, используемый в приложении.
/// </summary>
public sealed class DiscountInputEventArgs : EventArgs
{
    /// <summary>
    /// Размер пиццы.
    /// </summary>
    public int Size { get; }
    /// <summary>
    /// Процент скидки.
    /// </summary>
    public decimal Percent { get; }
    /// <summary>
    /// Выполняет операцию DiscountInputEventArgs в рамках данного компонента.
    /// </summary>
    public DiscountInputEventArgs(int size, decimal percent) { Size = size; Percent = percent; }
}
