namespace YukaBLL.Exceptions.Size
{
    public class SizeExistsException : Exception
    {
        /// <summary>
        /// Gets the name of the size that caused the exception.
        /// </summary>
        public string SizeName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeExistsException"/> class.
        /// </summary>
        /// <param name="sizeName">The Name of the size</param>
        public SizeExistsException(string sizeName) :
            base($"The size '{sizeName}' already exists. Please try a different name.")
        {
            SizeName = sizeName;
        }
    }
}
