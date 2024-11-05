namespace YukaBLL.Exceptions.ProductVariant
{
    /// <summary>
    /// Thrown when the stock of a product variant is below zero.
    /// </summary>
    public class StockBelowZeroException : Exception
    {
        /// <summary>
        /// Gets the stock of the product variant.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="StockBelowZeroException"/> class.
        /// </summary>
        /// <param name="stock">The stock provided when creating the new variant</param>
        public StockBelowZeroException(int stock) : 
            base($"The stock of the product variant is below zero: {stock}")
        {
            Stock = stock;
        }
    }
}
