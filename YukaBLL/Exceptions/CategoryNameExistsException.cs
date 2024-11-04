using System.Data;

namespace YukaBLL.Exceptions
{
    public class CategoryNameExistsException : Exception
    {
        public CategoryNameExistsException(string categoryName) : 
            base($"The category {categoryName} already exists, try a different name")
        {
        }
    }
}
