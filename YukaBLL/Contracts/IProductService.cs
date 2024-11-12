using YukaBLL.Core;
using YukaBLL.Dtos.Product;
using YukaBLL.Responses.Product;

namespace YukaBLL.Contracts
{
    public interface IProductService : IBaseService<ProductResponse>
    {
        /// <summary>
        /// Adds a new type of product to the database.
        /// </summary>
        /// <param name="addProductDto">The new product to be added.</param>
        /// <returns></returns>
        Task<ProductAddResponse> AddProductAsync(AddProductDto addProductDto);

        /// <summary>
        /// Updates the info regarding a product in the database.
        /// </summary>
        /// <param name="updateProductDto">New name, category or description for a product.</param>
        /// <returns></returns>
        Task<ProductUpdateResponse> UpdateProductAsync(UpdateProductDto updateProductDto);

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="deleteProductDto">Id of the product to be deleted.</param>
        /// <returns></returns>
        Task<ProductDeleteResponse> DeleteProductAsync(DeleteProductDto deleteProductDto);
    }
}
