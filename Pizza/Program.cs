using PizzaModel;
namespace Pizza
{
    internal class Program
    {
        static void Main()
        {
            var logic = new Logic();
            logic.Create("Пипперони", 500, true, 30);
            logic.Create("Маргарита", 350, false, 25);
            logic.Create("Песто", 700, true, 20);
            logic.Create("Грибная", 650, false, 30);
            while (true)
            {
                
                Console.WriteLine("\nПИЦЦЕРИЯ ОТ МАКСА");
                Console.WriteLine("1. Показать все пиццы");
                Console.WriteLine("2. Добавить пиццу");
                Console.WriteLine("3. Изменить пиццу");
                Console.WriteLine("4. Удалить пиццу");
                Console.WriteLine("5. Фильтр по размеру");
                Console.WriteLine("6. Сортировка по цене");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        foreach (var p in logic.ReadAll())
                            Console.WriteLine(p);
                        break;
                    case "2":
                        Console.Write("Название: ");
                        var n = Console.ReadLine();

                        Console.Write("Цена: ");
                        var pr = decimal.Parse(Console.ReadLine());

                        Console.Write("ПП Пицца? (y/n): ");
                        var v = Console.ReadLine().ToLower() == "y";

                        Console.Write("Размер (см): ");
                        var s = int.Parse(Console.ReadLine());

                        Console.WriteLine("Создано: " + logic.Create(n, pr, v, s));
                        break;
                    case "3":
                        Console.Write("Id пиццы: ");
                        var idU = int.Parse(Console.ReadLine());
                        Console.Write("Новое название: ");
                        var n2 = Console.ReadLine();
                        Console.Write("Новая цена: ");
                        var p2 = int.Parse(Console.ReadLine());
                        Console.Write("ПП Пицца?: ");
                        var v2 = Console.ReadLine().ToLower() == "y";
                        Console.Write("Новый размер: ");
                        var s2 = int.Parse(Console.ReadLine());
                        Console.WriteLine(logic.Update(idU, n2, p2, v2, s2) ? "Изменено" : "Не найдено");
                        break;
                    case "4":
                        Console.Write("Id пиццы: ");
                        var idDel = int.Parse(Console.ReadLine());
                        Console.WriteLine(logic.Delete(idDel) ? "Удалено" : "Не найдено");
                        break;
                    case "5":
                        Console.Write("Размер (см): ");
                        var sizeF = int.Parse(Console.ReadLine());

                        var f = logic.Filter_size(sizeF);

                        if (f.Count == 0)
                            Console.WriteLine("Ничего не найдено");
                        else
                            foreach (var p in f)
                                Console.WriteLine(p);
                        break;
                    case "6":
                        foreach (var p in logic.Sort_price())
                            Console.WriteLine(p);
                        break;
                    case "0":
                        return;

                }
            }
            

        }





    }
}
