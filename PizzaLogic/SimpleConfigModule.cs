using Ninject;
using Ninject.Modules;
using PizzaModel;
using DataAccessLayer;

namespace PizzaLogic
{
    /// <summary>
    /// Настройка зависимостей приложения.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            // Один общий контекст
            Bind<PizzaDbContext>()
                .ToSelf()
                .InSingletonScope();

            // Репозиторий пицц
            Bind<IRepository<Pizza>>()
                .To<EntityRepository<Pizza>>()
                .InSingletonScope();

            // Репозиторий скидок
            Bind<IRepository<SizeDiscount>>()
                .To<EntityRepository<SizeDiscount>>()
                .InSingletonScope();

            // Управление транзакциями
            Bind<IUnitOfWork>()
                .To<EntityUnitOfWork>()
                .InSingletonScope();

            // Бизнес логика
            Bind<Logic>()
                .ToSelf();
        }
    }
}