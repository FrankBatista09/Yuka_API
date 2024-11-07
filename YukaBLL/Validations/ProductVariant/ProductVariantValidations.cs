using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaBLL.Core;
using YukaBLL.Dtos.ProductVariant;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations.ProductVariant
{
    public class ProductVariantValidations
    {
        private ServiceResult IsValidProductVariant(ProductVariantDto productVariantDto)
        {
            ServiceResult result = new ServiceResult();

            if (productVariantDto.ProductId == null)
            {
                result.Success = false;
                result.Message = "The product is required.";
                return result;
            }
            
            if (productVariantDto.Quantity == null)
            {
                result.Success = false;
                result.Message = "The quantity is required.";
                return result;
            }
            
            if (productVariantDto.BrandId == null)
            {
                result.Success = false;
                result.Message = "The brand is required.";
                return result;
            }
            
            if (productVariantDto.SizeId == null)
            {
                result.Success = false;
                result.Message = "The size is required.";
                return result;
            }

            if (productVariantDto.ColorId == null)
            {
                result.Success = false;
                result.Message = "The color is required.";
                return result;
            }

            if (productVariantDto.Price == null)
            {
                result.Success = false;
                result.Message = "The price is required.";
                return result;
            }
            return result;
        }
    }
}
