namespace YukaBLL.Exceptions.Color
{
    /// <summary>
    /// Exception thrown when a color with the specified name already exists in the database.
    /// </summary>
    public class ColorNameExistsException : Exception
    {
        /// <summary>
        /// Gets the name of the color that caused the exception.
        /// </summary>
        public string ColorName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorNameExistsException"/> class.
        /// </summary>
        /// <param name="colorName">The name of the color that already exists.</param>
        public ColorNameExistsException(string colorName) :
            base($"The color '{colorName}' already exists. Please try a different name.")
        {
            ColorName = colorName;
        }
    }
}
