using PizzaLogic;
using Shared;

namespace Presenter;

/// <summary>Посредник между View и бизнес-логикой: обрабатывает события и обновляет представление.</summary>
public sealed class ShowPresenter
{
    private readonly IView _view;
    private readonly ILogic _logic;

    /// <summary>
    /// Выполняет операцию ShowPresenter в рамках данного компонента.
    /// </summary>
    public ShowPresenter(IView view, ILogic logic)
    {
        _view = view ?? throw new ArgumentNullException(nameof(view));
        _logic = logic ?? throw new ArgumentNullException(nameof(logic));
        Subscribe();
    }

    /// <summary>
    /// Подписывает презентер на события представления и бизнес-логики.
    /// </summary>
    private void Subscribe()
    {
        _view.ViewLoaded += OnViewLoaded;
        _view.ViewClosed += OnViewClosed;
        _view.AddPizzaRequested += OnAddPizza;
        _view.UpdatePizzaRequested += OnUpdatePizza;
        _view.DeletePizzaRequested += OnDeletePizza;
        _view.RefreshRequested += OnRefresh;
        _view.FilterRequested += OnFilter;
        _view.StatisticsRequested += OnStatistics;
        _view.SaveDiscountRequested += OnSaveDiscount;
        _view.DeleteDiscountRequested += OnDeleteDiscount;
        _logic.PizzasChanged += OnPizzasChanged;
        _logic.DiscountsChanged += OnDiscountsChanged;
    }

    /// <summary>
    /// Отписывает презентер от событий представления и бизнес-логики.
    /// </summary>
    private void Unsubscribe()
    {
        _view.ViewLoaded -= OnViewLoaded;
        _view.ViewClosed -= OnViewClosed;
        _view.AddPizzaRequested -= OnAddPizza;
        _view.UpdatePizzaRequested -= OnUpdatePizza;
        _view.DeletePizzaRequested -= OnDeletePizza;
        _view.RefreshRequested -= OnRefresh;
        _view.FilterRequested -= OnFilter;
        _view.StatisticsRequested -= OnStatistics;
        _view.SaveDiscountRequested -= OnSaveDiscount;
        _view.DeleteDiscountRequested -= OnDeleteDiscount;
        _logic.PizzasChanged -= OnPizzasChanged;
        _logic.DiscountsChanged -= OnDiscountsChanged;
    }

    /// <summary>
    /// Обрабатывает событие загрузки представления и обновляет отображаемые данные.
    /// </summary>
    private void OnViewLoaded(object? sender, EventArgs e) => RefreshAll();
    /// <summary>
    /// Обрабатывает закрытие представления и снимает подписки на события.
    /// </summary>
    private void OnViewClosed(object? sender, EventArgs e) => Unsubscribe();
    /// <summary>
    /// Обрабатывает запрос обновления данных.
    /// </summary>
    private void OnRefresh(object? sender, EventArgs e) => RefreshAll();

    /// <summary>
    /// Обрабатывает запрос на добавление пиццы.
    /// </summary>
    private void OnAddPizza(object? sender, PizzaInputEventArgs e) => Execute(() =>
    {
        _logic.Create(e.Name, e.Price, e.IsHealthy, e.Size);
        _view.ShowMessage("Пицца добавлена.");
    });

    /// <summary>
    /// Обрабатывает запрос на изменение пиццы.
    /// </summary>
    private void OnUpdatePizza(object? sender, PizzaInputEventArgs e) => Execute(() =>
    {
        if (_logic.Update(e.Id, e.Name, e.Price, e.IsHealthy, e.Size))
            _view.ShowMessage("Пицца изменена.");
        else
            _view.ShowError("Пицца не найдена.");
    });

    /// <summary>
    /// Обрабатывает запрос на удаление пиццы.
    /// </summary>
    private void OnDeletePizza(object? sender, IdEventArgs e) => Execute(() =>
    {
        if (_logic.Delete(e.Id)) _view.ShowMessage("Пицца удалена.");
        else _view.ShowError("Пицца не найдена.");
    });

    /// <summary>
    /// Обрабатывает запрос фильтрации пицц по диапазону цен.
    /// </summary>
    private void OnFilter(object? sender, PriceRangeEventArgs e) => Execute(() =>
        _view.ShowPizzas(MapPizzas(_logic.FilterByPrice(e.MinPrice, e.MaxPrice))));

    /// <summary>
    /// Обрабатывает запрос получения статистики.
    /// </summary>
    private void OnStatistics(object? sender, EventArgs e) => Execute(() =>
        _view.ShowStatistics(_logic.GetStatistics()));

    /// <summary>
    /// Обрабатывает запрос сохранения правила скидки.
    /// </summary>
    private void OnSaveDiscount(object? sender, DiscountInputEventArgs e) => Execute(() =>
    {
        _logic.SaveSizeDiscount(e.Size, e.Percent);
        _view.ShowMessage($"Для размера {e.Size} см установлена скидка {e.Percent:0.##}%.");
    });

    /// <summary>
    /// Обрабатывает запрос удаления правила скидки.
    /// </summary>
    private void OnDeleteDiscount(object? sender, IdEventArgs e) => Execute(() =>
    {
        if (_logic.DeleteSizeDiscount(e.Id)) _view.ShowMessage("Правило скидки удалено.");
        else _view.ShowError("Правило скидки не найдено.");
    });

    /// <summary>
    /// Обрабатывает уведомление бизнес-логики об изменении списка пицц.
    /// </summary>
    private void OnPizzasChanged(object? sender, EventArgs e) => RefreshPizzas();
    /// <summary>
    /// Обрабатывает уведомление бизнес-логики об изменении правил скидок.
    /// </summary>
    private void OnDiscountsChanged(object? sender, EventArgs e) => RefreshDiscounts();

    /// <summary>
    /// Обновляет отображение списка пицц и правил скидок.
    /// </summary>
    private void RefreshAll() { RefreshPizzas(); RefreshDiscounts(); }

    /// <summary>
    /// Получает актуальный список пицц, преобразует его в DTO и передаёт представлению.
    /// </summary>
    private void RefreshPizzas() => _view.ShowPizzas(MapPizzas(_logic.ReadAll()));

    /// <summary>
    /// Преобразует доменные объекты пицц в объекты передачи данных.
    /// </summary>
    private List<PizzaDto> MapPizzas(IEnumerable<PizzaModel.Pizza> pizzas) => pizzas
        .Select(p => DtoMapper.ToDto(p, _logic.GetDiscountedPrice(p))).ToList();

    /// <summary>
    /// Получает актуальные правила скидок, преобразует их в DTO и передаёт представлению.
    /// </summary>
    private void RefreshDiscounts() => _view.ShowDiscounts(
        _logic.ReadDiscounts().Select(DtoMapper.ToDto).ToList());

    /// <summary>
    /// Выполняет действие и передаёт возникшую ошибку представлению.
    /// </summary>
    private void Execute(Action action)
    {
        try { action(); }
        catch (Exception ex) { _view.ShowError(ex.Message); }
    }
}
