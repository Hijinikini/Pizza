using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace PizzaModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Pizza : IDomainObject
    {
     
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Type { get; set; }
        public int Size { get; set; }
        public override string ToString()
        {
            string type = Type ? "ПП" : "Обычная";

            return $"[{Id}] {Name} | Размер: {Size} см | " +
                   $"Цена: {Price} руб. | Тип: {type}";
        }
    }
}