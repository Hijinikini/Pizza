using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaModel
{
    /// <summary>
    /// Базовый интерфейс для всех доменных сущностей.
    /// Гарантирует наличие уникального идентификатора Id.
    /// </summary>
    public interface IDomainObject
    {
        int Id { get; set; }
    }
}