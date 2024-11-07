using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaBLL.Core;
using YukaBLL.Dtos.ProductVariant;
using YukaBLL.Exceptions.ProductVariant;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations.ProductVariant
{
    public class ProductVariantValidations
    {
        private ServiceResult IsValidProductVariant(AddProductVariantDto addProductVariantDto)
        {
            ServiceResult result = new ServiceResult();

            if (addProductVariantDto.ProductId == null)
            {
                result.Success = false;
                result.Message = "The product is required.";
                return result;
            }
            
            //if (addProductVariantDto.Quantity == null)
            //{
            //    result.Success = false;
            //    result.Message = "The quantity is required.";
            //    return result;
            //}
            
            if (addProductVariantDto.BrandId == null)
            {
                result.Success = false;
                result.Message = "The brand is required.";
                return result;
            }
            
            if (addProductVariantDto.SizeId == null)
            {
                result.Success = false;
                result.Message = "The size is required.";
                return result;
            }

            if (addProductVariantDto.ColorId == null)
            {
                result.Success = false;
                result.Message = "The color is required.";
                return result;
            }

            if (addProductVariantDto.Price == null)
            {
                result.Success = false;
                result.Message = "The price is required.";
                return result;
            }
            return result;
        }

        public async Task<ServiceResult> IsValidProductToAdd(AddProductVariantDto addProductVariantDto, 
            IProductVariantRepository productVariantRepository)
        {
            ServiceResult result = IsValidProductVariant(addProductVariantDto);

            try
            {
                if (await productVariantRepository.ExistsAsync(pv => pv.ProductId == addProductVariantDto.ProductId && pv.BrandId == addProductVariantDto.BrandId
                                                                && pv.ColorId == addProductVariantDto.ColorId && pv.SizeId == addProductVariantDto.SizeId))
                    throw new ProductVariantExistsException(addProductVariantDto.ProductId, addProductVariantDto.BrandId, addProductVariantDto.ColorId, addProductVariantDto.SizeId);

                result.Message = "The product variant is valid to be added";
                return result;
            }
            catch (ProductVariantExistsException ex)
            {
                result.Success= false;
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

        public async Task<ServiceResult> IsValidProductVariantToUpdate (UpdateProductVariantDto updateProductVariantDto, 
            IProductVariantRepository productVariantRepository)
        {
            ServiceResult result = new ServiceResult();

            AddProductVariantDto addProductVariantDto = new()
            {
                ProductId = updateProductVariantDto.ProductId,
                BrandId = updateProductVariantDto.BrandId,
                ColorId = updateProductVariantDto.ColorId,
                SizeId = updateProductVariantDto.SizeId,
            };
            result = IsValidProductVariant(addProductVariantDto);

            try
            {
                if(await productVariantRepository.ExistsAsync(pv => pv.ProductId == updateProductVariantDto.ProductId && pv.BrandId == updateProductVariantDto.BrandId
                                                                && pv.ColorId == updateProductVariantDto.ColorId && pv.SizeId == updateProductVariantDto.SizeId))
                    throw new ProductVariantExistsException(updateProductVariantDto.ProductId, updateProductVariantDto.BrandId, updateProductVariantDto.ColorId, updateProductVariantDto.SizeId);

                result.Message = "The product variant is valid to be updated";
                return result;
            }
            catch (ProductVariantExistsException ex)
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

        public async Task<ServiceResult> IsValidProductVariantToSell(SellVariantDto sellVariantDto,
            IProductVariantRepository productVariantRepository)
        {
            ServiceResult result = new ServiceResult();

            if (sellVariantDto.Quantity == null || sellVariantDto.Quantity <= 0)
            {
                result.Success = false;
                result.Message = "The quantity must be greater than zero.";
                return result;
            }

            if (sellVariantDto.VariantId == null)
            {
                result.Success = false;
                result.Message = "The product variant is required";
                return result;
            }

            try
            {
                if (sellVariantDto.Quantity <= 0) 
                    throw new PurchaseBelowEqualsZeroException(sellVariantDto.Quantity);

                result.Message = "The product variant is valid to sell";
                return result;
            }
            catch (PurchaseBelowEqualsZeroException ex)
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
