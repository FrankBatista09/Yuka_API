namespace YukaBLL.Exceptions.ProductVariant
{
    /// <summary>
    /// Thrown when a product variant's price is below or equal to zero.
    /// </summary>
    public class PriceBelowEqualsZeroException : Exception
    {
        /// <summary>
        /// Gets the price that caused the exception
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Creates an instance of the <see cref="PriceBelowEqualsZeroException"/> class.
        /// </summary>
        /// <param name="price"></param>
        public PriceBelowEqualsZeroException(double price) :
            base($"The price of the product variant is below or equal to zero. Please enter a valid price.")
        {
            Price = price;
        }
    }
}
