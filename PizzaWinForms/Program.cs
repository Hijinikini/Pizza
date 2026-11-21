using Shared;
using Presenter;

namespace PizzaWinForms;

/// <summary>
/// Точка входа приложения, запускающая выбранное представление.
/// </summary>
internal static class Program
{
    [STAThread]
    /// <summary>
    /// Выполняет операцию Main в рамках данного компонента.
    /// </summary>
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        IView view = WinFormsViewFactory.Create();
        CompositionRoot.CreatePresenter(view);
        view.Run();
        CompositionRoot.Dispose();
    }
}
