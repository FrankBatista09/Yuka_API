using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaBLL.Core;
using YukaBLL.Dtos.SizeCategory;
using YukaBLL.Exceptions.SizeCategory;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations.SizeCategoryValidations
{
    public class SizeCategoryValidations
    {
        public async Task<ServiceResult> IsValidSizeCategoryToAdd (AddSizeCategoryDto addSizeCategoryDto, ISizeCategoryRepository sizeCategoryRepository)
        {
            ServiceResult result = new ServiceResult();

            if (addSizeCategoryDto.CategoryId == null)
            {
                result.Success = false;
                result.Message = "The category is required";
                return result;
            }

            if (addSizeCategoryDto.SizeId == null)
            {
                result.Success = false;
                result.Message = "The size is required";
                return result;
            }

            try
            {
                if (await sizeCategoryRepository.ExistsAsync(sc => sc.SizeId == addSizeCategoryDto.SizeId && sc.CategoryId == addSizeCategoryDto.CategoryId))
                    throw new SizeCategoryExistsException(addSizeCategoryDto.SizeId, addSizeCategoryDto.CategoryId);

                result.Message = "The size category is valid to add";
                return result;
            }
            catch (SizeCategoryExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex.Data;
                return result;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex.Data;
                return result;
            }
        }

        public async Task<ServiceResult> IsValidSizeCategoryToUpdate(UpdateSizeCategoryDto updateSizeCategoryDto, ISizeCategoryRepository sizeCategoryRepository)
        {
            ServiceResult result = new ServiceResult();

            if (updateSizeCategoryDto.CategoryId == null)
            {
                result.Success = false;
                result.Message = "The category is required";
                return result;
            }

            if (updateSizeCategoryDto.SizeId == null)
            {
                result.Success = false;
                result.Message = "The size is required";
                return result;
            }

            try
            {
                if (await sizeCategoryRepository.ExistsAsync(sc => sc.SizeId == updateSizeCategoryDto.SizeId && sc.CategoryId == updateSizeCategoryDto.CategoryId))
                    throw new SizeCategoryExistsException(updateSizeCategoryDto.CategoryId, updateSizeCategoryDto.SizeId);

                result.Message = "The size category is valid to add";
                return result;
            }
            catch (SizeCategoryExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex.Data;
                return result;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex.Data;
                return result;
            }
        }
    }
}
