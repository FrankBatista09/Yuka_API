using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface IProductBrandPriceGroupRepository : IBaseRepository<ProductBrandPriceGroup>
    {
        Task<ProductBrandPriceGroup> GetByBrandIdAsync(int brandId);
        Task<ProductBrandPriceGroup> GetByProductIdAsync(int productId);
    }
}
