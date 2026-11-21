using PizzaModel;
using Shared;

namespace Presenter;

/// <summary>Преобразует доменные объекты в DTO, чтобы View не получала объекты модели.</summary>
internal static class DtoMapper
{
    /// <summary>
    /// Преобразует доменный объект в соответствующий объект передачи данных.
    /// </summary>
    public static PizzaDto ToDto(Pizza pizza, decimal discountedPrice) => new()
    {
        Id = pizza.Id, Name = pizza.Name, Price = pizza.Price,
        DiscountedPrice = discountedPrice, IsHealthy = pizza.Type, Size = pizza.Size
    };

    /// <summary>
    /// Преобразует доменный объект в соответствующий объект передачи данных.
    /// </summary>
    public static DiscountDto ToDto(SizeDiscount discount) => new()
    {
        Id = discount.Id, Size = discount.Size, DiscountPercent = discount.DiscountPercent
    };
}
