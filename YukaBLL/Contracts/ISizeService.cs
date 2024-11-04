using YukaBLL.Core;
using YukaBLL.Dtos.Size;
using YukaBLL.Responses.Size;

namespace YukaBLL.Contracts
{
    public interface ISizeService : IBaseService<SizeResponse>
    {
        /// <summary>
        /// Add a new size to the database.
        /// </summary>
        /// <param name="addSizeDto">New size to add.</param>
        /// <returns></returns>
        Task<SizeAddResponse> AddSizeAsync(AddSizeDto addSizeDto);

        /// <summary>
        /// Updates the size name.
        /// </summary>
        /// <param name="updateSizeDto">New name for the size.</param>
        /// <returns></returns>
        Task<SizeUpdateResponse> UpdateSizeAsync(UpdateSizeDto updateSizeDto);

        /// <summary>
        /// Deletes a size from the database.
        /// </summary>
        /// <param name="deleteSizeDto">Id of the size.</param>
        /// <returns></returns>
        Task<SizeDeleteResponse> DeleteSizeAsync(DeleteSizeDto deleteSizeDto);
    }
}
