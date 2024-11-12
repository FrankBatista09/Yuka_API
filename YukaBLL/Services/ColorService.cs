using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaBLL.Contracts;
using YukaBLL.Dtos.Brand;
using YukaBLL.Dtos.Color;
using YukaBLL.Responses.Color;
using YukaBLL.Validations.ColorValidations;
using YukaDAL.Entities;
using YukaDAL.Interfaces;
using YukaDAL.Repositories;

namespace YukaBLL.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly ILogger<ColorService> _logger;

        public ColorService (IColorRepository colorRepository, ILogger<ColorService> logger)
        {
            _colorRepository = colorRepository;
            _logger = logger;
        }
        public async Task<ColorAddResponse> AddColorAsync(AddColorDto addColorDto)
        {
            ColorAddResponse result = new ColorAddResponse();

            try
            {
                var isValidToAdd = await ColorValidations.IsValidColorToAdd(addColorDto, _colorRepository);
                if (isValidToAdd.Success)
                {
                    YukaDAL.Entities.Color color = new()
                    {
                        ColorName = addColorDto.ColorName,
                        CreatedBy = addColorDto.CreatedBy,
                    };
                    result.Data = await _colorRepository.CreateAsync(color);
                    result.Message = "The color was added successfully";
                    return result;
                }
                result.Success = false;
                result.Message = isValidToAdd.Message;
                result.Data = isValidToAdd.Data;
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ColorDeleteResponse> DeleteColorAsync(DeleteColorDto deleteColorDto)
        {
            ColorDeleteResponse result = new ColorDeleteResponse();
            try
            {
                var colorfound = await _colorRepository.GetByIdAsync(deleteColorDto.ColorId);

                if (colorfound != null)
                {
                    YukaDAL.Entities.Color color = new()
                    {
                        ColorId = deleteColorDto.ColorId,
                        DeletedBy = deleteColorDto.DeletedBy,
                    };
                    await _colorRepository.DeleteAsync(color);
                    result.Message = "Color deleted successfully";
                    return result;
                }
                result.Success = false;
                result.Message = $"No brand found with ID {deleteColorDto.ColorId}";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ColorResponse> GetAllAsync()
        {
            ColorResponse result = new ColorResponse();

            try
            {
                var colors = await _colorRepository.GetAllAsync();

                if (colors.Any())
                {
                    result.Data = (
                                from color in colors
                                where color.IsDeleted = false
                                select new ColorDto
                                {
                                    ColorId = color.ColorId,
                                    ColorName = color.ColorName,
                                }
                        ).ToList();

                    result.Message = "Colors retrieved successfully";
                    return result;
                }
                result.Success = false;
                result.Message = "No color found";
                return result;  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ColorResponse> GetByIdAsync(int id)
        {
            ColorResponse result = new ColorResponse();

            try
            {
                var colors = await _colorRepository.GetByIdAsync(id);
                
                if (colors != null)
                {
                    result.Data = new ColorDto
                    {
                        ColorId= colors.ColorId,
                        ColorName = colors.ColorName,
                    };

                    result.Message = "Brand retrieved successfully";
                    return result;
                }
                result.Success = false;
                result.Message = "Color not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ColorUpdateResponse> UpdateColorAsync(UpdateColorDto updateColorDto)
        {
            ColorUpdateResponse result = new ColorUpdateResponse();

            try
            {
                var colorToUpdate = await _colorRepository.GetByIdAsync(updateColorDto.ColorId);
                if (colorToUpdate != null)
                {
                    var isValidToUpdate = await ColorValidations.IsValidColorToUpdate(updateColorDto, _colorRepository);

                    if (isValidToUpdate.Success)
                    {
                        YukaDAL.Entities.Color color = new()
                        {
                            ColorId = updateColorDto.ColorId,
                            ColorName = updateColorDto.ColorName,
                            UpdatedBy = updateColorDto.UpdatedBy,
                        };
                        await _colorRepository.UpdateAsync(color);
                        result.Message = "Color Updated successfully";
                        return result;
                    }
                    result.Success = false;
                    result.Message = isValidToUpdate.Message;
                    result.Data = isValidToUpdate.Data;
                    return result;
                }
                result.Success = false;
                result.Message = "The color does not exists";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
