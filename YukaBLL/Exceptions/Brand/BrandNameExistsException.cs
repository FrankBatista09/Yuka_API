namespace YukaBLL.Exceptions.Brand
{
    /// <summary>
    /// Exception thrown when a brand with the specified name already exists in the database.
    /// </summary>
    public class BrandNameExistsException : Exception
    {
        /// <summary>
        /// Gets the name of the brand that caused the exception.
        /// </summary>
        public string BrandName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandNameExistsException"/> class.
        /// </summary>
        /// <param name="brandName">The name of the brand that already exists.</param>
        public BrandNameExistsException(string brandName) :
            base($"The brand '{brandName}' already exists. Please try a different name.")
        {
            BrandName = brandName;
        }
    }
}
