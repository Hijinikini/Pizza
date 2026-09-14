using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaModel
{
  
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Type { get; set; }
        public int Size { get; set; }
        public override string ToString()
        {
            string type = Type ? "ПП":"" ;
            return $"[{Id}] {Name} ({Size}) — {Price} руб. {type}\n ";
        }
    }
     
    
}

