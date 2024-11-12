using YukaBLL.Core;
using YukaBLL.Dtos.SizeCategory;
using YukaBLL.Responses.Size;
using YukaBLL.Responses.SizeCategory;

namespace YukaBLL.Contracts
{
    public interface ISizeCategoryService : IBaseService<SizeCategoryResponse>
    {
        /// <summary>
        /// Gets all the sizes by category id.
        /// </summary>
        /// <param name="categoryId">The id of the category to retrieve the sizes of.</param>
        /// <returns>A list of the sizes under that category.</returns>
        Task<SizeResponse> GetSizesByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Adds a new size category to the database.
        /// </summary>
        /// <param name="addSizeCategoryDto">New size to be added to the database.</param>
        /// <returns></returns>
        Task<SizeCategoryAddResponse> AddSizeCategoryAsync(AddSizeCategoryDto addSizeCategoryDto);

        /// <summary>
        /// Updates the category of the size provided.
        /// </summary>
        /// <param name="updateSizeCategoryDto">New category for the size provided.</param>
        /// <returns></returns>
        Task<SizeCategoryUpdateResponse> UpdateSizeCategoryAsync(UpdateSizeCategoryDto updateSizeCategoryDto);

        /// <summary>
        /// Deletes a size category from the database.
        /// </summary>
        /// <param name="deleteSizeCategoryDto">Size id and Category id</param>
        /// <returns></returns>
        Task<SizeCategoryDeleteResponse> DeleteSizeCategoryAsync(DeleteSizeCategoryDto deleteSizeCategoryDto);
    }
}
