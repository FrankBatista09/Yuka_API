using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        /// <summary>
        /// Adds a category with its size
        /// </summary>
        /// <param name="newCategory"></param>
        /// <param name="selectedSizeIds"></param>
        /// <returns></returns>
        Task CreateCategoryWithSizesAsync(Category newCategory, List<Size> selectedSizeIds);
    }
}
