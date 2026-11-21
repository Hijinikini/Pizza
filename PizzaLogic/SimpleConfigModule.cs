
using Ninject.Modules;
using DataAccessLayer;

namespace PizzaLogic
{
    /// <summary>
    /// Модуль настройки внедрения зависимостей.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Настраивает привязки интерфейсов к реализациям.
        /// </summary>
        public override void Load()
        {
            Bind<PizzaDbContext>()
                .ToSelf()
                .InSingletonScope();

            Bind<IUnitOfWork>()
                .To<EntityUnitOfWork>()
                .InSingletonScope();

            Bind<IPizzaLogic>()
                .To<PizzaService>()
                .InSingletonScope();

            Bind<IDiscountLogic>()
                .To<DiscountService>()
                .InSingletonScope();

            Bind<ILogic>()
                .To<Logic>()
                .InSingletonScope();
        }
    }
}
