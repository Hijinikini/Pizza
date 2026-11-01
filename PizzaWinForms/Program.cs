using System;
using System.Windows.Forms;
using Ninject;
using PizzaLogic;

namespace PizzaWinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (IKernel ninjectKernel =
                new StandardKernel(new SimpleConfigModule()))
            {
                Logic logic = ninjectKernel.Get<Logic>();

                Application.Run(new Form1(logic));
            }
        }
    }
}