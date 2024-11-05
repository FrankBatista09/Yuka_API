using Microsoft.IdentityModel.Tokens;
using YukaBLL.Core;
using YukaBLL.Dtos.Category;
using YukaBLL.Exceptions.Category;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations
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
                if(await categoryRepository.ExistsAsync(category => category.CategoryName == addCategoryDto.CategoryName))
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
    }
}
