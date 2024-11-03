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
        /// <param name="id">Id of the product variant</param>
        /// <param name="newPrice">New price of the variant</param>
        /// <returns></returns>
        Task UpdatePriceAsync(int id, double newPrice);

        /// <summary>
        /// Updates the stock of a product variant
        /// </summary>
        /// <param name="id">Id of the variant</param>
        /// <param name="newStock">Quantity to be put in stock</param>
        /// <returns></returns>
        Task UpdateStockAsync(int id, int newStock);

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
        /// <param name="id">Id of the product variant</param>
        /// <param name="quantity">Quantity to be added to stock</param>
        /// <returns></returns>
        Task AddToStock(int id, int quantity);

        /// <summary>
        /// Gets the quantity of a product variant
        /// </summary>
        /// <param name="variantId"></param>
        /// <returns></returns>
        Task<int> GetQuantityAsync(int variantId);

    }
}
