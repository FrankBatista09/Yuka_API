using Microsoft.Extensions.Logging;
using YukaBLL.Contracts;
using YukaBLL.Dtos.Size;
using YukaBLL.Dtos.SizeCategory;
using YukaBLL.Responses.Size;
using YukaBLL.Responses.SizeCategory;
using YukaBLL.Validations.SizeCategoryValidations;
using YukaDAL.Interfaces;


namespace YukaBLL.Services
{
    public class SizeCategoryService : ISizeCategoryService
    {
        private readonly ISizeCategoryRepository _sizeCategoryRepository;
        private readonly ILogger<SizeCategoryService> _logger;

        public SizeCategoryService(ISizeCategoryRepository sizeCategoryRepository, ILogger<SizeCategoryService> logger)
        {
            _sizeCategoryRepository = sizeCategoryRepository;
            _logger = logger;
        }
        public async Task<SizeCategoryAddResponse> AddSizeCategoryAsync(AddSizeCategoryDto addSizeCategoryDto)
        {
            SizeCategoryAddResponse result = new();

            try
            {
                var validToAdd = await SizeCategoryValidations.IsValidSizeCategoryToAdd(addSizeCategoryDto, _sizeCategoryRepository);

                if (validToAdd.Success)
                {
                    YukaDAL.Entities.SizeCategory sizeCategory = new()
                    {
                        CategoryId = addSizeCategoryDto.CategoryId,
                        SizeId = addSizeCategoryDto.SizeId,
                        CreatedBy = addSizeCategoryDto.CreatedBy
                    };
                    result.Data = await _sizeCategoryRepository.CreateAsync(sizeCategory);
                    result.Message = "Size Category added successfully";
                }
                result.Message = validToAdd.Message;
                result.Success = false;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<SizeCategoryDeleteResponse> DeleteSizeCategoryAsync(DeleteSizeCategoryDto deleteSizeCategoryDto)
        {
            SizeCategoryDeleteResponse result = new();
            try
            {
                var sizeCategoryToDelete = await _sizeCategoryRepository.GetByIdsAsync(deleteSizeCategoryDto.CategoryId, deleteSizeCategoryDto.SizeId);
                if (sizeCategoryToDelete != null)
                {
                    YukaDAL.Entities.SizeCategory sizeCategory = new()
                    {
                        CategoryId = deleteSizeCategoryDto.CategoryId,
                        SizeId = deleteSizeCategoryDto.SizeId,
                        DeletedBy = deleteSizeCategoryDto.DeletedBy
                    };
                    await _sizeCategoryRepository.DeleteAsync(sizeCategory);
                    result.Message = "Size Category deleted successfully";
                    return result;
                }
            
                    result.Message = "Size Category not found";
                    return result;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<SizeCategoryResponse> GetAllAsync()
        {
            SizeCategoryResponse result = new();

            try
            {
                var sizeCategories = await _sizeCategoryRepository.GetAllAsync();

                result.Data = (from sizeCategory in sizeCategories
                               where sizeCategory.IsDeleted == false
                               select new SizeCategoryDto() 
                               { 
                                   CategoryId = sizeCategory.CategoryId, 
                                   SizeId = sizeCategory.SizeId,
                               }).ToList();
                result.Message = "Size Categories retrieved successfully";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task<SizeCategoryResponse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SizeResponse> GetSizesByCategoryIdAsync(int categoryId)
        {
            SizeResponse result = new();
            try
            {
                var sizes = await _sizeCategoryRepository.SizeByCategory(categoryId);
                result.Data = (from size in sizes
                               where size.IsDeleted == false
                               select new SizeDto()
                               {
                                   SizeId = size.SizeId,
                                   SizeName = size.SizeName
                               }).ToList();
                result.Message = "Sizes retrieved successfully";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<SizeCategoryUpdateResponse> UpdateSizeCategoryAsync(UpdateSizeCategoryDto updateSizeCategoryDto)
        {
            SizeCategoryUpdateResponse result = new();

            try
            {
                var sizeCategoryToUpdate = await _sizeCategoryRepository
                    .GetByIdsAsync(updateSizeCategoryDto.CategoryId, updateSizeCategoryDto.SizeId);

                if(sizeCategoryToUpdate != null)
                {
                    var validToUpdate = await SizeCategoryValidations.IsValidSizeCategoryToUpdate(updateSizeCategoryDto, _sizeCategoryRepository);
                    if (validToUpdate.Success) 
                    { 
                        YukaDAL.Entities.SizeCategory sizeCategory = new()
                        {
                            CategoryId = updateSizeCategoryDto.CategoryId,
                            SizeId = updateSizeCategoryDto.SizeId,
                            UpdatedBy = updateSizeCategoryDto.UpdatedBy
                        };
                        await _sizeCategoryRepository.UpdateAsync(sizeCategory);
                        result.Message = "Size Category updated successfully";
                        return result;
                    }
                    result.Message = validToUpdate.Message;
                    result.Success = false;
                    return result;
                }
                result.Message = "Size Category not found";
                result.Success = false;
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
