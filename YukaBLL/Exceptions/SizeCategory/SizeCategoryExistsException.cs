namespace YukaBLL.Exceptions.SizeCategory
{
    /// <summary>
    /// Thrown when a size already belongs to a category in the database.
    /// </summary>
    public class SizeCategoryExistsException : Exception
    {
        /// <summary>
        /// Gets the size ID that caused the exception.
        /// </summary>
        public int SizeId { get;  }
        /// <summary>
        /// Gets the category ID that caused the exception.
        /// </summary>
        public int CategoryId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeCategoryExistsException"/> class.
        /// </summary>
        /// <param name="sizeId">The id of the size to add to the category</param>
        /// <param name="categoryId">The id of the category where the size is to be added</param>
        public SizeCategoryExistsException(int sizeId, int categoryId) :
            base($"The size with ID {sizeId} already belongs to the category with ID {categoryId}. Please try a different category.")
        {
            SizeId = sizeId;
            CategoryId = categoryId;
        }
    }
}
