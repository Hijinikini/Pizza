namespace PizzaWinForms
{
    internal static class Program
    {
        /// <summary>
        /// Главный метод программы.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}