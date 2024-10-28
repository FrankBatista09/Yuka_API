using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface IProductVariantRepository : IBaseRepository<ProductVariant>
    {
        Task<List<ProductVariant>> GetByBrandAndColorAsync(int brandId, int colorId);

    }
}
