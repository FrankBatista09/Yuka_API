using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaBLL.Core;
using YukaBLL.Dtos.Color;
using YukaBLL.Exceptions.Color;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations.ColorValidations
{
    public class ColorValidations
    {
        public static async Task<ServiceResult> IsValidColorToAdd (AddColorDto addColorDto, IColorRepository colorRepository)
        {
            ServiceResult result = new ServiceResult ();

            if (addColorDto.ColorName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The color name is required";
                return result;
            }

            try
            {
                if (await colorRepository.ExistsAsync(c => c.ColorName == addColorDto.ColorName))
                    throw new ColorNameExistsException(addColorDto.ColorName);


                result.Message = "Color is valid to add";
                return result;
            }
            catch (ColorNameExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex.Data;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while validating the color";
                result.Data = ex.Data;
                return result ;
            }
        }

        public static async Task<ServiceResult> IsValidColorToUpdate (UpdateColorDto updateColorDto, IColorRepository colorRepository)
        {
            ServiceResult result = new ServiceResult ();

            if (updateColorDto.ColorName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The color name is required to update";
                return result;
            }

            try
            {
                if (await colorRepository.ExistsAsync(c => c.ColorName == updateColorDto.ColorName))
                    throw new ColorNameExistsException (updateColorDto.ColorName);

                result.Message = "The color is valid to update";
                return result;
            }
            catch (ColorNameExistsException ex)
            {
                result.Success= false;
                result.Message = ex.Message;
                result.Data = ex.Data;
                return result; 
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while validating the color";
                result.Data = ex.Data;
                return result;
            }
                
                

        }
    }
}
