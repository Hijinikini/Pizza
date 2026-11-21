namespace Shared;

/// <summary>Контракт представления. Presenter зависит от него, а не от конкретной Windows Form.</summary>
public interface IView
{
    /// <summary>
    /// Событие, возникающее после загрузки представления.
    /// </summary>
    event EventHandler? ViewLoaded;
    /// <summary>
    /// Событие, возникающее при закрытии представления.
    /// </summary>
    event EventHandler? ViewClosed;
    /// <summary>
    /// Событие запроса на добавление пиццы со стороны представления.
    /// </summary>
    event EventHandler<PizzaInputEventArgs>? AddPizzaRequested;
    /// <summary>
    /// Событие запроса на изменение пиццы со стороны представления.
    /// </summary>
    event EventHandler<PizzaInputEventArgs>? UpdatePizzaRequested;
    /// <summary>
    /// Событие запроса на удаление пиццы со стороны представления.
    /// </summary>
    event EventHandler<IdEventArgs>? DeletePizzaRequested;
    /// <summary>
    /// Событие запроса на обновление отображаемых данных.
    /// </summary>
    event EventHandler? RefreshRequested;
    /// <summary>
    /// Событие запроса фильтрации пицц по диапазону цен.
    /// </summary>
    event EventHandler<PriceRangeEventArgs>? FilterRequested;
    /// <summary>
    /// Событие запроса отображения статистики.
    /// </summary>
    event EventHandler? StatisticsRequested;
    /// <summary>
    /// Событие запроса сохранения правила скидки.
    /// </summary>
    event EventHandler<DiscountInputEventArgs>? SaveDiscountRequested;
    /// <summary>
    /// Событие запроса удаления правила скидки.
    /// </summary>
    event EventHandler<IdEventArgs>? DeleteDiscountRequested;

    void ShowPizzas(IReadOnlyList<PizzaDto> pizzas);
    void ShowDiscounts(IReadOnlyList<DiscountDto> discounts);
    void ShowStatistics(string statistics);
    void ShowMessage(string message);
    void ShowError(string message);
    void Run();
}
