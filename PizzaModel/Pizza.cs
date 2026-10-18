namespace PizzaModel
{
    /// <summary>
    /// Сущность "Пицца".
    /// </summary>
    public class Pizza : IDomainObject
    {
        /// <summary>
        /// Id пиццы.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена в рублях.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// ПП или обычная.
        /// </summary>
        public bool Type { get; set; }

        /// <summary>
        /// Размер в см.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Вывод пиццы в виде строки.
        /// </summary>
        /// <returns>Текст с данными пиццы.</returns>
        public override string ToString()
        {
            string type = Type ? "ПП" : "Обычная";

            return $"[{Id}] {Name} | Размер: {Size} см | " +
                   $"Цена: {Price} руб. | Тип: {type}";
        }
    }
}