using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaModel
{
    /// <summary>
    /// Пицца.
    /// </summary>
    public class Pizza
    {
        /// <summary>
        /// ID пиццы.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название пиццы.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена пиццы.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Показывает, является ли пицца ПП.
        /// </summary>
        public bool Type { get; set; }

        /// <summary>
        /// Размер пиццы.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Выводит информацию о пицце.
        /// </summary>
        public override string ToString()
        {
            string type = Type ? "ПП" : "Обычная";

            return $"[{Id}] {Name} | Размер: {Size} см | " +
                   $"Цена: {Price} руб. | Тип: {type}";
        }
    }
}