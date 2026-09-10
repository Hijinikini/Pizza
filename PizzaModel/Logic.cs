using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaModel
{
  public class Logic
    {
        private readonly List<Pizza> _pizzas = new List<Pizza>();
        private int _nextId = 1;
        public Pizza Create(string name, decimal price, bool type, int size)
        {
            var pizza = new Pizza
            {
                Id = _nextId++,
                Name = name,
                Price = price,
                Type = type,
                Size = size
            };
            _pizzas.Add(pizza);
            return pizza;
        }
        public Pizza Read(int id)
        {
            return _pizzas.FirstOrDefault(p => p.Id == id);
        }
        public List<Pizza> ReadAll()
        {
            return new List<Pizza>(_pizzas);
        }
        public bool Update(int id, string name, decimal price, bool type, int size)
        {
            var pizza = Read(id);
            if (pizza == null) return false;

            pizza.Name = name;
            pizza.Price = price;
            pizza.Type = type;
            pizza.Size = size;
            return true;
        }
        public bool Delete(int id)
        {
            var pizza = Read(id);
            if (pizza == null) return false;
            _pizzas.Remove(pizza);
            return true;
        }
        public List<Pizza> Filter_size(int size)
        {
            return _pizzas.Where(p => p.Size == size).ToList();
        }
        public List<Pizza> Sort_price()
        {
            return _pizzas.OrderBy(p => p.Price).ToList();
        }
    }
}
