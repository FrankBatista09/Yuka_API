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
        /// <returns></returns>
        Task<List<ProductVariant>> GetByBrandIdAsync(int brandId);

        /// <summary>
        /// Gets a list of product variants by brand, color and product type
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="colorId"></param>
        /// <param name="productId"></param>
        /// <returns>List of ProductVariant</returns>
        Task<List<ProductVariant>> GetBySpecificAsync(int brandId, int colorId, int productId);

    }
}
