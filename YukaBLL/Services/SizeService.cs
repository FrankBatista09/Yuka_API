using Microsoft.Extensions.Logging;
using YukaBLL.Contracts;
using YukaBLL.Dtos.Size;
using YukaBLL.Responses.Size;
using YukaBLL.Validations.SizeValidations;
using YukaDAL.Interfaces;

namespace YukaBLL.Services
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly ILogger<SizeService> _logger;

        public SizeService(ISizeRepository sizeRepository, ILogger<SizeService> logger)
        {
            _sizeRepository = sizeRepository;
            _logger = logger;
        }

        public async Task<SizeAddResponse> AddSizeAsync(AddSizeDto addSizeDto)
        {
            SizeAddResponse result = new();

            try
            {
                var IsValidToAdd = await SizeValidations.IsValidSizeToAdd(addSizeDto, _sizeRepository);

                if (IsValidToAdd.Success)
                {
                    YukaDAL.Entities.Size size = new()
                    {
                        SizeName = addSizeDto.SizeName,
                        CreatedBy = addSizeDto.CreatedBy,
                    };

                    result.Data = await _sizeRepository.CreateAsync(size);
                    result.Message = "Size created successfully";
                    return result;
                }
                result.Success = false;     
                result.Message = IsValidToAdd.Message;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<SizeDeleteResponse> DeleteSizeAsync(DeleteSizeDto deleteSizeDto)
        {
            SizeDeleteResponse result = new();
            try
            {
                var sizeFound = await _sizeRepository.GetByIdAsync(deleteSizeDto.SizeId);

                if (sizeFound != null) 
                {

                    YukaDAL.Entities.Size size = new()
                    {
                        SizeId = sizeFound.SizeId,
                        DeletedBy = sizeFound.DeletedBy,
                    };

                    await _sizeRepository.DeleteAsync(size);

                    result.Message = "Size successfully deleted";
                    return result;
                }

                result.Success = false;
                result.Message = $"No size found with Id {deleteSizeDto.SizeId}";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<SizeResponse> GetAllAsync()
        {
            SizeResponse result = new();

            try
            {
                var sizes = await _sizeRepository.GetAllAsync();

                if (sizes.Any())
                {
                    result.Data = (from size in sizes
                                   where size.IsDeleted == false
                                   select new SizeDto
                                   {
                                       SizeId = size.SizeId,
                                       SizeName = size.SizeName
                                   }).ToList();
                    result.Message = "Sizes retrieved successfully";
                    return result; 
                }
                result.Success = false;
                result.Message = "No sizes found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); 
                throw;
            }
        }

        public async Task<SizeResponse> GetByIdAsync(int id)
        {
            SizeResponse result = new();

            try
            {
                var size = await _sizeRepository.GetByIdAsync(id);

                if (size != null)
                {
                    result.Data = new SizeDto()
                    {
                        SizeId = size.SizeId,
                        SizeName = size.SizeName
                    };

                    result.Message = "Size retrieved successfully";
                    return result; 
                }
                result.Success = false;
                result.Message = "Product not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<SizeUpdateResponse> UpdateSizeAsync(UpdateSizeDto updateSizeDto)
        {
            SizeUpdateResponse result = new();
            try
            {
                var IsValidToUpdate = await SizeValidations.IsValidSizeToUpdate(updateSizeDto, _sizeRepository);

                if (IsValidToUpdate.Success)
                {
                    YukaDAL.Entities.Size size = new()
                    {
                        SizeId = updateSizeDto.SizeId,
                        SizeName = updateSizeDto.SizeName,
                        UpdatedBy = updateSizeDto.UpdatedBy,
                    };

                    await _sizeRepository.UpdateAsync(size);

                    result.Message = "Size updated successfully";
                    return result;
                }
                result.Success = false;
                result.Message = IsValidToUpdate.Message;
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
