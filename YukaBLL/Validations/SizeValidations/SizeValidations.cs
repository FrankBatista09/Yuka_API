using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaBLL.Core;
using YukaBLL.Dtos.Size;
using YukaBLL.Exceptions.Size;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations.SizeValidations
{
    public class SizeValidations
    {
        public async Task<ServiceResult> IsValidSizeToAdd(AddSizeDto addSizeDto, ISizeRepository sizeRepository)
        {
            ServiceResult result = new ServiceResult ();

            if (addSizeDto.SizeName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The size is requiered";
                return result;
            }

            try
            {
                if (await sizeRepository.ExistsAsync(s => s.SizeName == addSizeDto.SizeName)) 
                    throw new SizeExistsException (addSizeDto.SizeName);

                result.Message = "The size is valid to add";
                return result;
            }
            catch (SizeExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex.Data;
                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while validating size";
                result.Data = ex.Data;
                return result;
                
            }
        }

        public async Task<ServiceResult> IsValidSizeToUpdate(UpdateSizeDto updateSizeDto, ISizeRepository sizeRepository)
        {
            ServiceResult result = new ServiceResult ();

            if(updateSizeDto.SizeName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The size is requiered";
                return result;
            }

            try
            {
                if(await sizeRepository.ExistsAsync(s => s.SizeName == updateSizeDto.SizeName))
                    throw new SizeExistsException(updateSizeDto.SizeName);

                result.Message = "The size is valid to update";
                return result;
            }
            catch (SizeExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;    
                result.Data = ex.Data;
                return result;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "An error occurred while validating the size";
                result.Data = ex.Data;
                return result;
            }
        }
    }
}
