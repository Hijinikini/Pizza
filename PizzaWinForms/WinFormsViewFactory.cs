using Shared;

namespace PizzaWinForms;

/// <summary>Создаёт представление и возвращает его через общий интерфейс IView.</summary>
public static class WinFormsViewFactory
{
    /// <summary>
    /// Создаёт новую пиццу и возвращает созданный объект.
    /// </summary>
    public static IView Create() => new Form1();
}
