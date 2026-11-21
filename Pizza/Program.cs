using Presenter;
using Shared;

namespace Pizza;

/// <summary>Точка входа консольного приложения.</summary>
internal static class Program
{
    /// <summary>
    /// Выполняет операцию Main в рамках данного компонента.
    /// </summary>
    private static void Main()
    {
        IView view = new ConsoleView();
        CompositionRoot.CreatePresenter(view);
        view.Run();
        CompositionRoot.Dispose();
    }
}
