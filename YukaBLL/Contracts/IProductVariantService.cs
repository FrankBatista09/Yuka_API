using YukaBLL.Core;
using YukaBLL.Dtos.ProductVariant;
using YukaBLL.Responses.ProductVariant;

namespace YukaBLL.Contracts
{
    public interface IProductVariantService : IBaseService<ProductVariantResponse>
    {
        /// <summary>
        /// Add a new product variant to an already existing product variant.
        /// </summary>
        /// <param name="addProductVariantDto">The new variant to be added.</param>
        /// <returns></returns>
        Task<ProductVariantAddResponse> AddProductVariantAsync(AddProductVariantDto addProductVariantDto);

        /// <summary>
        /// Updates a product variant.
        /// </summary>
        /// <param name="updateProductVariantDto">The product variant to be updated.</param>
        /// <returns></returns>
        Task<ProductVariantUpdateResponse> UpdateProductVariantAsync(UpdateProductVariantDto updateProductVariantDto);

        /// <summary>
        /// Deletes a product variant.
        /// </summary>
        /// <param name="deleteProductVariantDto">The product variant to be deleted.</param>
        /// <returns></returns>
        Task<ProductVariantDeleteResponse> DeleteProductVariantAsync(DeleteProductVariantDto deleteProductVariantDto);

        /// <summary>
        /// Updates the price of a product variant.
        /// </summary>
        /// <param name="updatePriceDto">The id and the new price of the variant to be updated.</param>
        /// <returns></returns>
        Task<ProductVariantResponse> UpdatePriceAsync(UpdatePriceDto updatePriceDto);

        /// <summary>
        /// When creating a new variant, adds all the possible variants at once to the database.
        /// </summary>
        /// <param name="addProductVariantDtos">List of the variants to add.</param>
        /// <returns></returns>
        Task<ProductVariantAddResponse> AddBulkAsync(List<AddProductVariantDto> addProductVariantDtos);

        /// <summary>
        /// Used when overwriting the current stock, this updates the stock of a product variant.
        /// </summary>
        /// <param name="updateStockDto">The id and the new stock to be placed in the database.</param>
        /// <returns></returns>
        Task<ProductVariantUpdateResponse> UpdateStockAsync(UpdateStockDto updateStockDto);

        /// <summary>
        /// Adds a quantity to the stock of a product variant.
        /// </summary>
        /// <param name="addToStockDto">The id and the ammount to be added to the product variant.</param>
        /// <returns></returns>
        Task<ProductVariantUpdateResponse> AddToStockAsync(AddStockDto addToStockDto);

        /// <summary>
        /// Sells a product variant, which subtracts the ammount provided from the stock.
        /// </summary>
        /// <param name="sellVariantDto">the id of the variant and the ammount to sell.</param>
        /// <returns></returns>
        Task<ProductVariantResponse> SellAsync(SellVariantDto sellVariantDto);
    }
}
