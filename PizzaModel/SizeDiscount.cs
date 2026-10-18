namespace PizzaModel
{
    /// <summary>
    /// Скидка на размер пиццы.
    /// </summary>
    public class SizeDiscount : IDomainObject
    {
        /// <summary>
        /// Id скидки.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Размер в см.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Процент скидки (0-100).
        /// </summary>
        public decimal DiscountPercent { get; set; }

        /// <summary>
        /// Вывод скидки в виде строки.
        /// </summary>
        /// <returns>Текст с размером и процентом.</returns>
        public override string ToString()
        {
            return $"{Size} см — скидка {DiscountPercent:0.##}%";
        }
    }
}