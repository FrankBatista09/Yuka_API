using Microsoft.IdentityModel.Tokens;
using YukaBLL.Core;
using YukaBLL.Dtos.Product;
using YukaBLL.Exceptions.Product;
using YukaDAL.Interfaces;

namespace YukaBLL.Validations
{
    public class ProductValidations
    {
        /// <summary>
        /// Validates if the product is valid to add.
        /// </summary>
        /// <param name="addProductDto"></param>
        /// <param name="productRepository"></param>
        /// <returns></returns>
        public static async Task<ServiceResult> IsValidProductToAdd(AddProductDto addProductDto,
            IProductRepository productRepository)
        {
            ServiceResult result = new();

            if (addProductDto.ProductName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The product name is required.";
                return result;
            }

            try
            {
                if (await productRepository.ExistsAsync(product => product.ProductName == addProductDto.ProductName))
                    throw new ProductNameExistsException(addProductDto.ProductName);

                result.Message = "Product is valid to add.";
                return result;
            }
            catch (ProductNameExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while validating the product.";
                result.Data = ex;
                return result;
            }

        }

        /// <summary>
        /// Validates if the product is valid to update.
        /// </summary>
        /// <param name="updateProductDto"></param>
        /// <param name="productRepository"></param>
        /// <returns></returns>
        public static async Task<ServiceResult> IsValidProductToUpdate(UpdateProductDto updateProductDto,
            IProductRepository productRepository)
        {
            ServiceResult result = new();

            if (updateProductDto.ProductName.IsNullOrEmpty())
            {
                result.Success = false;
                result.Message = "The product name is required.";
                return result;
            }

            try
            {
                if (await productRepository.ExistsAsync(product => product.ProductName == updateProductDto.ProductName))
                    throw new ProductNameExistsException(updateProductDto.ProductName);

                result.Message = "Product is valid to update.";
                return result;
            }
            catch (ProductNameExistsException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Data = ex;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while validating the product.";
                result.Data = ex;
                return result;
            }

        }
    }
}
