using Microsoft.Extensions.Logging;
using YukaBLL.Contracts;
using YukaBLL.Dtos.Category;
using YukaBLL.Responses.Category;
using YukaBLL.Validations.CategoryValidations;
using YukaDAL.Interfaces;

namespace YukaBLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly ILogger<CategoryService> _logger;
        
        public CategoryService (ICategoryRepository categoryRepository, ILogger<CategoryService> logger, ISizeRepository sizeRepository)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _sizeRepository = sizeRepository;

        }
        public async Task<CategoryAddResponse> AddCategoryAsync(AddCategoryDto addCategoryDto)
        {
            CategoryAddResponse result = new CategoryAddResponse();

            try
            {
                var isValidToAdd = await CategoryValidations.IsValidCategoryToAdd(addCategoryDto, _categoryRepository);

                if (isValidToAdd.Success)
                {
                    YukaDAL.Entities.Category category = new()
                    {
                        CategoryName = addCategoryDto.CategoryName,
                        CreatedBy = addCategoryDto.CreatedBy,
                    };

                    result.Data = await _categoryRepository.CreateAsync(category);
                    result.Message = "Category added successfully";
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

        public async Task<CategoryWithSizeAddResponse> AddCategoryWithSize(AddCategoryWithSizesDto addCategoryWithSizesDto)
        {
            CategoryWithSizeAddResponse result = new CategoryWithSizeAddResponse();

            try
            {
                var isValidToAddWithSize = await CategoryValidations.IsValidToAddCategoryWithSize(
                    addCategoryWithSizesDto, _categoryRepository, _sizeRepository);

                if (isValidToAddWithSize.Success)
                {
                    YukaDAL.Entities.Category category = new()
                    {
                        CategoryName = addCategoryWithSizesDto.CategoryName,
                        CreatedBy = addCategoryWithSizesDto.CreatedBy
                    };
                    
                    await _categoryRepository.CreateCategoryWithSizesAsync(category, addCategoryWithSizesDto.SelectedSizeIds);

                    result.Message = "Category with sizes added successfully";
                    return result;
                }
                result.Success = false;
                result.Message = isValidToAddWithSize.Message;
                result.Data = isValidToAddWithSize.Data;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<CategoryDeleteResponse> DeleteCategoryAsync(DeleteCategoryDto deleteCategoryDto)
        {
            CategoryDeleteResponse result = new CategoryDeleteResponse();

            try
            {
                var categorytoDelete = await _categoryRepository.GetByIdAsync(deleteCategoryDto.CategoryId);

                if (categorytoDelete != null)
                {
                    YukaDAL.Entities.Category category = new()
                    {
                        CategoryId = categorytoDelete.CategoryId,
                        DeletedBy = deleteCategoryDto.DeletedBy,
                    };
                    await _categoryRepository.DeleteAsync(category);
                    result.Message = "Category deleted successfully";
                    return result;
                }
                result.Success = false;
                result.Message = $"No category found with the ID {deleteCategoryDto.CategoryId}";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<CategoryResponse> GetAllAsync()
        {
            CategoryAddResponse result = new();

            try
            {
                var categories = await _categoryRepository.GetAllAsync();

                if (categories.Any())
                {
                    result.Data = (
                                from category in categories
                                where category.IsDeleted = false
                                select new CategoryDto
                                {
                                    CategoryId = category.CategoryId,
                                    CategoryName = category.CategoryName,
                                }).ToList();
                    result.Message = "Categories retrieved successfully";
                    return result;
                }
                result.Success = false;
                result.Message = "No categories found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            CategoryAddResponse result = new CategoryAddResponse();

            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);

                if (category != null)
                {
                    result.Data = new CategoryDto 
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                    };

                    result.Message = "Category retrieved successfully";
                    return result;
                }
                result.Success = false;
                result.Message = "Category not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<CategoryUpdateResponse> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            CategoryUpdateResponse result = new CategoryUpdateResponse();

            try
            {
                var isValidToUpdate = await CategoryValidations.IsValidCategoryToUpdate(updateCategoryDto, _categoryRepository);

                if(isValidToUpdate.Success)
                {
                        YukaDAL.Entities.Category category = new()
                        {
                            CategoryId = updateCategoryDto.CategoryId,
                            CategoryName = updateCategoryDto.CategoryName,
                            UpdatedBy = updateCategoryDto.UpdatedBy,
                        };
                        await _categoryRepository.UpdateAsync(category);
                        result.Message = "Category updated successfully";
                        return result;
                }
                result.Success= false;
                result.Message = isValidToUpdate.Message;
                result.Data = isValidToUpdate.Data;
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
