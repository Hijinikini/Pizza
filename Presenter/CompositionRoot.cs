using Ninject;
using PizzaLogic;
using Shared;

namespace Presenter;

/// <summary>Единственное место сборки зависимостей: создаёт бизнес-логику и связывает её с View через Presenter.</summary>
public static class CompositionRoot
{
    private static IKernel? _kernel;

    /// <summary>
    /// Создаёт контейнер зависимостей, получает бизнес-логику и создаёт презентер для указанного представления.
    /// </summary>
    public static ShowPresenter CreatePresenter(IView view)
    {
        _kernel = new StandardKernel(new SimpleConfigModule());
        ILogic logic = _kernel.Get<ILogic>();
        return new ShowPresenter(view, logic);
    }

    /// <summary>
    /// Освобождает ресурсы объекта и связанные с ним подключения.
    /// </summary>
    public static void Dispose() { _kernel?.Dispose(); _kernel = null; }
}
