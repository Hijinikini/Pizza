using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaModel
{
    /// <summary>
    /// Доменная сущность "Пицца". Содержит Id, Name, Price, Type, Size.
    /// Id — уникальный идентификатор. Name — название. Price — цена в рублях.
    /// Type — ПП или обычная. Size — размер в см.
    /// </summary>
    public class Pizza : IDomainObject
    {
     
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Type { get; set; }
        public int Size { get; set; }

        /// <summary>
        /// Возвращает строковое представление пиццы.
        /// </summary>
        /// <returns>Строка с Id, названием, размером, ценой и типом.</returns>
        public override string ToString()
        {
            string type = Type ? "ПП" : "Обычная";

            return $"[{Id}] {Name} | Размер: {Size} см | " +
                   $"Цена: {Price} руб. | Тип: {type}";
        }
    }
}