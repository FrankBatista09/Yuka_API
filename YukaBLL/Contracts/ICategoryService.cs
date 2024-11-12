using YukaBLL.Core;
using YukaBLL.Dtos.Category;
using YukaBLL.Responses.Category;

namespace YukaBLL.Contracts
{
    public interface ICategoryService : IBaseService<CategoryResponse>
    {
        /// <summary>
        /// Adds a new category of sizes to the database.
        /// </summary>
        /// <param name="addCategoryDto">The new category to add in the database.</param>
        /// <returns></returns>
        Task<CategoryAddResponse> AddCategoryAsync(AddCategoryDto addCategoryDto);

        /// <summary>
        /// Updates the name of a category of sizes
        /// </summary>
        /// <param name="updateCategoryDto">The new name of the category.</param>
        /// <returns></returns>
        Task<CategoryUpdateResponse> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);

        /// <summary>
        /// Deletes a category of sizes from the database.
        /// </summary>
        /// <param name="deleteCategoryDto">The id of the category to be deleted.</param>
        /// <returns></returns>
        Task<CategoryDeleteResponse> DeleteCategoryAsync(DeleteCategoryDto deleteCategoryDto);
        
        /// <summary>
        /// Creates a category with its own sizes
        /// </summary>
        /// <param name="addCategoryWithSizesDto">The new cateogry to add and the list of sizes for the category</param>
        /// <returns></returns>
        Task<CategoryWithSizeAddResponse> AddCategoryWithSize(AddCategoryWithSizesDto addCategoryWithSizesDto);
    }
}
