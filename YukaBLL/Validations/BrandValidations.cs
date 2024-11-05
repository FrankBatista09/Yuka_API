using Microsoft.IdentityModel.Tokens;
using YukaBLL.Core;
using YukaBLL.Dtos.Brand;
using YukaBLL.Exceptions.Brand;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations
{
    public class BrandValidations
    {
        /// <summary>
        /// Validates a brand when adding it or updating it.
        /// </summary>
        /// <param name="addBrandDto"></param>
        /// <param name="brandRepository"></param>
        /// <returns></returns>
        public static async Task<ServiceResult> IsValidBrandToAdd(AddBrandDto addBrandDto, IBrandRepository brandRepository)
        {
            ServiceResult result = new();

            // Verify if the brand name is empty or null
            if (addBrandDto.BrandName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The brand name is required.";
                return result;
            }

            try
            {
                // Verify if the brand already exists
                if (await brandRepository.ExistsAsync(brand => brand.BrandName == addBrandDto.BrandName))
                    throw new BrandNameExistsException(addBrandDto.BrandName);
                

                result.Message = "Brand is valid to add.";
                return result;
            }
            catch (BrandNameExistsException ex)
            {
                // Capture the exception
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex;
                return result;
            }
            catch (Exception ex)
            {
                // Capture no controlled exception
                result.Success = false;
                result.Message = "An error occurred while validating the brand.";
                result.Data = ex;
                return result;
            }
        }


        /// <summary>
        /// Validates a brand when updating it.
        /// </summary>
        /// <param name="updateBrandDto"></param>
        /// <param name="brandRepository"></param>
        /// <returns></returns>
        public static async Task<ServiceResult> IsValidBrandToUpdate(UpdateBrandDto updateBrandDto, IBrandRepository brandRepository)
        {
            ServiceResult result = new();

            // Verify if the brand name is empty or null
            if (updateBrandDto.BrandName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The brand name is required.";
                return result;
            }

            try
            {
                // Verify if the brand already exists
                if (await brandRepository.ExistsAsync(brand => brand.BrandName == updateBrandDto.BrandName))
                {
                    throw new BrandNameExistsException(updateBrandDto.BrandName);
                }

                result.Success = true;
                result.Message = "Brand is valid to update.";
                return result;
            }
            catch (BrandNameExistsException ex)
            {
                // Capture the exception
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex;
                return result;
            }
            catch (Exception ex)
            {
                // Capture no controlled exception
                result.Success = false;
                result.Message = "An error occurred while validating the brand.";
                result.Data = ex;
                return result;
            }
        }
    }
}
