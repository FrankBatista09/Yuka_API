using System.Data;

namespace YukaBLL.Exceptions.Category
{
    /// <summary>
    /// Exception thrown when a category with the specified name already exists in the database.
    /// </summary>
    public class CategoryNameExistsException : Exception
    {
        /// <summary>
        /// Gets the name of the category that caused the exception.
        /// </summary>
        public string CategoryName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryNameExistsException"/> class.
        /// </summary>
        /// <param name="categoryName">The name of the category that already exists.</param>
        public CategoryNameExistsException(string categoryName) :
            base($"The category '{categoryName}' already exists. Please try a different name.")
        {
            CategoryName = categoryName;
        }
    }
}
