namespace PizzaModel
{
    /// <summary>
    /// Правило скидки для определённого размера пиццы.
    /// Содержит Id, Size (размер в см) и DiscountPercent (процент скидки).
    /// </summary>
    public class SizeDiscount : IDomainObject
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public decimal DiscountPercent { get; set; }

        /// <summary>
        /// Возвращает строковое представление правила скидки.
        /// </summary>
        /// <returns>Строка вида "15 см — скидка 10%".</returns>
        public override string ToString()
        {
            return $"{Size} см — скидка {DiscountPercent:0.##}%";
        }
    }
}