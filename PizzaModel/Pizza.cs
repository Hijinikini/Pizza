using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Pizza
    {
     
        public int Id { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string type = Type ? "ПП" : "Обычная";

            return $"[{Id}] {Name} | Размер: {Size} см | " +
                   $"Цена: {Price} руб. | Тип: {type}";
        }
    }
}