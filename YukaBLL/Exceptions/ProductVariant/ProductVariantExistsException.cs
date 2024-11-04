namespace YukaBLL.Exceptions.ProductVariant
{
    /// <summary>
    /// Exception thrown when a product variant already exists in the database.
    /// </summary>
    public class ProductVariantExistsException : Exception
    {
        /// <summary>
        /// Gets the product ID of the product variant that caused the exception.
        /// </summary>
        public int ProductId { get; }
        /// <summary>
        /// Gets the brand ID of the product variant that caused the exception.
        /// </summary>
        public int BrandId { get; }
        /// <summary>
        /// Gets the color ID of the product variant that caused the exception.
        /// </summary>
        public int ColorId { get; }
        /// <summary>
        /// Gets the size ID of the product variant that caused the exception.
        /// </summary>
        public int SizeId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductVariantExistsException"/> class.
        /// </summary>
        /// <param name="productId">Product ID.</param>
        /// <param name="brandId">Brand ID.</param>
        /// <param name="colorId">Color ID.</param>
        /// <param name="sizeId">Size ID.</param>
        public ProductVariantExistsException(int productId, int brandId, int colorId, int sizeId) :
            base($"The product variant with product ID {productId}, brand ID {brandId}, " +
                 $"color ID {colorId}, and size ID {sizeId} already exists. Please try a different variant.")
        {
            ProductId = productId;
            BrandId = brandId;
            ColorId = colorId;
            SizeId = sizeId;
        }
    }
}
