namespace YukaBLL.Exceptions.Product
{
    /// <summary>
    /// Exception thrown when a product with the specified name already exists in the database.
    /// </summary>
    public class ProductNameExistsException : Exception
    {
        /// <summary>
        /// Gets the name of the product that caused the exception.
        /// </summary>
        public string ProductName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNameExistsException"/> class.
        /// </summary>
        /// <param name="productName">The name of the product that already exists.</param>
        public ProductNameExistsException(string productName) :
            base($"The product '{productName}' already exists. Please try a different name.")
        {
            ProductName = productName;
        }
    }
}
