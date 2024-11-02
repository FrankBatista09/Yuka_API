using System.Linq.Expressions;
using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface IProductVariantRepository : IBaseRepository<ProductVariant>
    {
        /// <summary>
        /// Gets a list of product variants by brand and color
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="colorId"></param>
        /// <returns>List of ProductVariant</returns>
        Task<List<ProductVariant>> GetBySpecificAsync(int brandId, int colorId);

        /// <summary>
        /// Gets a list of product variants by brand
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>The products that belong to the brand given</returns>
        Task<List<ProductVariant>> GetByBrandIdAsync(int brandId);

        /// <summary>
        /// Gets a list of product variants by brand, color and product type
        /// </summary>
        /// <param name="expression"> Lambda expression indicating the specifics to get the productVariant</param>
        /// <returns>List of ProductVariant</returns>
        Task<List<ProductVariant>> GetBySpecificAsync(Expression<Func<ProductVariant, bool>> expression);

    }
}
