using System.Linq.Expressions;
using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface IProductBrandPriceGroupRepository : IBaseRepository<ProductBrandPriceGroup>
    {
        /// <summary>
        /// Gets the prices based on an expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>ProductBrandPriceGroup</returns>
        Task<ProductBrandPriceGroup> GetByExpressionAsync(Expression<Func<ProductBrandPriceGroup, ProductBrandPriceGroup>> expression);

    }
}
