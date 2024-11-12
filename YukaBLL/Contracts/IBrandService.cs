using YukaBLL.Core;
using YukaBLL.Dtos.Brand;
using YukaBLL.Responses.Brand;

namespace YukaBLL.Contracts
{
    public interface IBrandService : IBaseService<BrandResponse>
    {
        /// <summary>
        /// Add a new brand.
        /// </summary>
        /// <param name="addBrandDto">The new brand to be added.</param>
        /// <returns></returns>
        Task<BrandAddResponse> AddBrandAsync(AddBrandDto addBrandDto);

        /// <summary>
        /// Updates the brand name.
        /// </summary>
        /// <param name="updateBrandDto">The new brand name and its ID.</param>
        /// <returns></returns>
        Task<BrandUpdateResponse> UpdateBrandAsync(UpdateBrandDto updateBrandDto);

        /// <summary>
        /// Deletes a brand from the database.
        /// </summary>
        /// <param name="deleteBrandDto">The Id of the brand.</param>
        /// <returns></returns>
        Task<BrandDeleteResponse> DeleteBrandAsync(DeleteBrandDto deleteBrandDto);
    }
}
