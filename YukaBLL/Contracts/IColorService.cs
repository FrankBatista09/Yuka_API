using YukaBLL.Core;
using YukaBLL.Dtos.Color;
using YukaBLL.Responses.Color;

namespace YukaBLL.Contracts
{
    public interface IColorService : IBaseService<ColorResponse>
    {
        /// <summary>
        /// Adds a new color to the database.
        /// </summary>
        /// <param name="addColorDto">The new color to be added.</param>
        /// <returns></returns>
        Task<ColorAddResponse> AddColorAsync(AddColorDto addColorDto);

        /// <summary>
        /// Updates the color name.
        /// </summary>
        /// <param name="updateColorDto">The the id of the color and its new name.</param>
        /// <returns></returns>
        Task<ColorUpdateResponse> UpdateColorAsync(UpdateColorDto updateColorDto);

        /// <summary>
        /// Deletes a color from the database.
        /// </summary>
        /// <param name="deleteColorDto">The id of the color to be deleted.</param>
        /// <returns></returns>
        Task<ColorDeleteResponse> DeleteColorAsync(DeleteColorDto deleteColorDto);
    }
}
