using Microsoft.IdentityModel.Tokens;
using YukaBLL.Core;
using YukaBLL.Dtos.Category;
using YukaBLL.Exceptions.Category;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations.CategoryValidations
{
    public class CategoryValidations
    {
        public static async Task<ServiceResult> IsValidCategoryToAdd(AddCategoryDto addCategoryDto,
            ICategoryRepository categoryRepository)
        {
            ServiceResult result = new();

            if (addCategoryDto.CategoryName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The category name is required.";
                return result;
            }

            try
            {
                if (await categoryRepository.ExistsAsync(category => category.CategoryName == addCategoryDto.CategoryName))
                    throw new CategoryNameExistsException(addCategoryDto.CategoryName);

                result.Message = "Category is valid to add.";
                return result;
            }
            catch (CategoryNameExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while validating the brand.";
                result.Data = ex;
                return result;
            }

        }

        public static async Task<ServiceResult> IsValidCategoryToUpdate(UpdateCategoryDto updateCategoryDto,
            ICategoryRepository categoryRepository)
        {
            ServiceResult result = new();

            if (updateCategoryDto.CategoryName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The category name is required.";
                return result;
            }

            try
            {
                if (await categoryRepository.ExistsAsync(category => category.CategoryName == updateCategoryDto.CategoryName))
                    throw new CategoryNameExistsException(updateCategoryDto.CategoryName);

                result.Message = "Category is valid to update.";
                return result;
            }
            catch (CategoryNameExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while validating the brand.";
                result.Data = ex;
                return result;
            }
        }

        public async Task<ServiceResult> IsValidToAddCategoryWithSize (AddCategoryWithSizesDto addCategoryWithSizesDto, ICategoryRepository categoryRepository, ISizeRepository sizeRepository)
        {
            ServiceResult result = new();

            var availableSizes = await sizeRepository.GetAllAsync();

            if (addCategoryWithSizesDto.CategoryName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The category name is required.";
                return result;
            }

            if (addCategoryWithSizesDto.SelectedSizeIds.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "At least one size need to be selected.";
                return result;
            }
            // Check if the selected sizes are between 1 and the available sizes
            if (addCategoryWithSizesDto.SelectedSizeIds.Count < 1 || addCategoryWithSizesDto.SelectedSizeIds.Count > availableSizes.Count)
            {
                result.Success = false;
                result.Message = $"You must select between 1 and {availableSizes.Count} of the avaiables sizes.";
                return result;
            }
            // Check if the selected sizes are not duplicated
            if (addCategoryWithSizesDto.SelectedSizeIds.Distinct().Count() != addCategoryWithSizesDto.SelectedSizeIds.Count)
            {
                result.Success = false;
                result.Message = "You cannot select the same size more than once.";
                return result;
            }

            try
            {
                if (await categoryRepository.ExistsAsync(category => category.CategoryName == addCategoryWithSizesDto.CategoryName))
                    throw new CategoryNameExistsException(addCategoryWithSizesDto.CategoryName);

                result.Message = "Category is valid to be added";
                return result;
            }
            catch(CategoryNameExistsException ce)
            {
                result.Success = false;
                result.Message = ce.Message;
                result.Data = ce;
                return result;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "An error occurred while validating the category.";
                result.Data = ex;
                return result;
            }
        }
    }
}
