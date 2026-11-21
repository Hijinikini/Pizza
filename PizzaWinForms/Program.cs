
using System;
using System.Windows.Forms;
using Ninject;
using PizzaLogic;

namespace PizzaWinForms
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Запускает приложение и настраивает внедрение зависимостей.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (var ninjectKernel = new StandardKernel(new SimpleConfigModule()))
            {
                Form1 form = ninjectKernel.Get<Form1>();
                Application.Run(form);
            }
        }
    }
}
