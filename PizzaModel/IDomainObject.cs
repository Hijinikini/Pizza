
namespace PizzaModel
{
    /// <summary>
    /// Базовый интерфейс для всех доменных сущностей.
    /// Гарантирует наличие уникального идентификатора Id.
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// </summary>
        int Id { get; set; }
    }
}
