using System.Linq.Expressions;
using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface IProductVariantRepository : IBaseRepository<ProductVariant>
    {
        /// <summary>
        /// Gets a list of product variants by brand, color and product type
        /// </summary>
        /// <param name="expression"> Lambda expression indicating the specifics to get the productVariant</param>
        /// <returns>List of ProductVariant</returns>
        Task<List<ProductVariant>> GetBySpecificAsync(Expression<Func<ProductVariant, bool>> expression);

        /// <summary>
        /// Updates the price of a product variant
        /// </summary>
        /// <param name="entity">Entity with the new Price</param>
        /// <returns></returns>
        Task UpdatePriceAsync(ProductVariant entity);

        /// <summary>
        /// Updates the stock of a product variant
        /// </summary>
        /// <param name="entity">Entity with the new Stock</param>
        /// <returns></returns>
        Task UpdateStockAsync(ProductVariant entity);

        /// <summary>
        /// Sells a product variant
        /// </summary>
        /// <param name="id">Id of the product variant</param>
        /// <param name="quantity">quantity to be removed from the stock</param>
        /// <returns></returns>
        Task SellAsync(int id, int quantity);

        /// <summary>
        /// Adds a quantity to the stock of a product variant
        /// </summary>
        /// <param name="entity">Entity with the stock to be added</param>
        /// <returns></returns>
        Task AddToStock(ProductVariant entity);

        /// <summary>
        /// Add multiple rows
        /// </summary>
        /// <param name="entities">List of entities to add</param>
        /// <returns></returns>
        Task BulkCreateAsync(List<ProductVariant> entities);

    }
}
