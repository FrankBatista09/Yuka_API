using Microsoft.Extensions.Logging;
using YukaBLL.Contracts;
using YukaBLL.Dtos.Product;
using YukaBLL.Responses.Product;
using YukaBLL.Validations.ProductValidations;
using YukaDAL.Interfaces;

namespace YukaBLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ProductAddResponse> AddProductAsync(AddProductDto addProductDto)
        {
            ProductAddResponse result = new();
            try
            {
                var IsValidToAdd = await ProductValidations.IsValidProductToAdd(addProductDto, _productRepository);

                if (IsValidToAdd.Success) 
                {
                    YukaDAL.Entities.Product product = new()
                    {
                        ProductName = addProductDto.ProductName,
                        Description = addProductDto.ProductDescription,
                        CategoryId = addProductDto.CategoryId,
                        CreatedBy = addProductDto.CreatedBy,
                    };
                    
                    result.Data = await _productRepository.CreateAsync(product);
                    result.Message = "Product created successfully";
                    return result;
                }
                result.Success = false;
                result.Message = IsValidToAdd.Message;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductDeleteResponse> DeleteProductAsync(DeleteProductDto deleteProductDto)
        {
            ProductDeleteResponse result = new();

            try
            {
                var productFound = await _productRepository.GetByIdAsync(deleteProductDto.ProductId);
                
                if(productFound != null)
                {
                    YukaDAL.Entities.Product product = new() 
                    { 
                        ProductId = deleteProductDto.ProductId,
                        DeletedBy = deleteProductDto.DeletedBy,
                    };

                    await _productRepository.DeleteAsync(product);

                    result.Message = "Product deleted successfully";
                    return result;
                }
                result.Success= false;
                result.Message = "Product not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductResponse> GetAllAsync()
        {
            ProductResponse result = new();
            try
            {
                var products = await _productRepository.GetAllAsync();

                    result.Data = (from product in products
                                   where product.IsDeleted == false
                                   select new ProductDto()
                                   {
                                       ProductId = product.ProductId,
                                       ProductName = product.ProductName,
                                       CategoryId = product.CategoryId,
                                       Description = product.Description,
                                   }).ToList();

                    result.Message = "Products retrieved successfully";
                    return result;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            ProductResponse result = new();

            try
            {
                var product = await _productRepository.GetByIdAsync(id);

                if(product != null)
                {
                    result.Data = new ProductDto()
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                    };

                    result.Message = "Product retrieved successfully";
                    return result;
                }

                result.Success = false;
                result.Message = "Product not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductUpdateResponse> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            ProductUpdateResponse result = new();

            try
            {
                var productToUpdate = await _productRepository.GetByIdAsync(updateProductDto.ProductId);

                if (productToUpdate != null) 
                {
                    var validToUpdate = await ProductValidations.IsValidProductToUpdate(updateProductDto, _productRepository);
                    if (validToUpdate.Success)
                    {
                        YukaDAL.Entities.Product product = new()
                        {
                            ProductId = updateProductDto.ProductId,
                            ProductName = updateProductDto.ProductName,
                            Description = updateProductDto.ProductDescription,
                            UpdatedBy = updateProductDto.UpdatedBy,
                            CategoryId = updateProductDto.CategoryId,
                        };

                        await _productRepository.UpdateAsync(product);

                        result.Message = "Product updated successfully";
                        return result;
                    }
                    result.Success = false;
                    result.Message = validToUpdate.Message;
                    return result;
                }
                result.Success = false;
                result.Message = "The product does not exists";
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
