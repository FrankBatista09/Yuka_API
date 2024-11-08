using Microsoft.Extensions.Logging;
using YukaBLL.Contracts;
using YukaBLL.Dtos.Product;
using YukaBLL.Dtos.ProductVariant;
using YukaBLL.Responses.ProductVariant;
using YukaBLL.Validations.ProductVariant;
using YukaDAL.Interfaces;

namespace YukaBLL.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ILogger<ProductVariantService> _logger;

        public ProductVariantService(IProductVariantRepository productVariantRepository, ILogger<ProductVariantService> logger)
        {
            _productVariantRepository = productVariantRepository;
            _logger = logger;
        }
        public async Task<ProductVariantAddResponse> AddBulkAsync(List<AddProductVariantDto> addProductVariantDtos)
        {
            ProductVariantAddResponse result = new ProductVariantAddResponse();
            try
            {
                List<YukaDAL.Entities.ProductVariant> productVariants = new();

                foreach (var item in addProductVariantDtos)
                {
                    var isValidProductVariant = await ProductVariantValidations.IsValidProductVariantToAdd(item, _productVariantRepository);

                    if (isValidProductVariant.Success)
                    {
                        YukaDAL.Entities.ProductVariant productVariant = new()
                        {
                            ProductId = item.ProductId,
                            SizeId = item.SizeId,
                            ColorId = item.ColorId,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            BrandId = item.BrandId,
                            CreatedBy = item.CreatedBy
                        };
                        productVariants.Add(productVariant);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = isValidProductVariant.Message;
                        result.Data = isValidProductVariant.Data;
                        return result;
                    }
                }
                if (productVariants.Any())
                {
                    await _productVariantRepository.BulkCreateAsync(productVariants);
                    result.Message = "Product variants added successfully";
                    return result;
                }
                else
                {
                    result.Success = false;
                    result.Message = "No product variant added";
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantAddResponse> AddProductVariantAsync(AddProductVariantDto addProductVariantDto)
        {
            ProductVariantAddResponse result = new ProductVariantAddResponse();

            try
            {
                var isValidProductVariant = await ProductVariantValidations.IsValidProductVariantToAdd(addProductVariantDto, _productVariantRepository);

                if (isValidProductVariant.Success)
                {
                    YukaDAL.Entities.ProductVariant productVariant = new()
                    {
                        ProductId = addProductVariantDto.ProductId,
                        SizeId = addProductVariantDto.SizeId,
                        ColorId = addProductVariantDto.ColorId,
                        Price = addProductVariantDto.Price,
                        Quantity = addProductVariantDto.Quantity,
                        CreatedBy = addProductVariantDto.CreatedBy
                    };
                    result.Data = await _productVariantRepository.CreateAsync(productVariant);
                    result.Message = "Product variant added successfully";
                    return result;
                }
                result.Success = false;
                result.Message = isValidProductVariant.Message;
                result.Data = isValidProductVariant.Data;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantUpdateResponse> AddToStockAsync(AddStockDto addToStockDto)
        {
            ProductVariantUpdateResponse result = new ProductVariantUpdateResponse();

            try
            {
                var stokcToUpdate = await _productVariantRepository.GetByIdAsync(addToStockDto.ProductVariantId);
                if (stokcToUpdate != null)
                {
                    var isValidStock = await ProductVariantValidations.IsValidStockToAdd(addToStockDto, _productVariantRepository);
                    if (isValidStock.Success)
                    {
                        YukaDAL.Entities.ProductVariant productVariant = new()
                        {
                            Quantity = addToStockDto.StockToAdd,
                            UpdatedBy = addToStockDto.UpdatedBy
                        };
                        await _productVariantRepository.AddToStock(productVariant);
                        result.Message = "Quantity updated successfully";
                        return result;
                    }
                    result.Success = false;
                    result.Message = isValidStock.Message;
                    result.Data = isValidStock.Data;
                    return result;
                }
                result.Success = false;
                result.Message = "The produc variant does not exist";
                return result;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantDeleteResponse> DeleteProductVariantAsync(DeleteProductVariantDto deleteProductVariantDto)
        {
            ProductVariantDeleteResponse result = new ProductVariantDeleteResponse();

            try
            {
                var productvariantToDelete = await _productVariantRepository.GetByIdAsync(deleteProductVariantDto.ProductVariantId);

                if (productvariantToDelete != null)
                {
                    YukaDAL.Entities.ProductVariant productvariant = new()
                    {
                        VariantId = deleteProductVariantDto.ProductVariantId,
                        DeletedBy = deleteProductVariantDto.DeletedBy
                    };
                    await _productVariantRepository.DeleteAsync(productvariantToDelete);
                    result.Message = "Product variant deleted successfully";
                    return result;
                }
                result.Success = false;
                result.Message = $"Product variant with id {deleteProductVariantDto.ProductVariantId} not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantResponse> GetAllAsync()
        {
            ProductVariantAddResponse result = new ProductVariantAddResponse();

            try
            {
                var productvariants = await _productVariantRepository.GetAllAsync();

                if (productvariants.Any())
                {
                    result.Data = (
                        from productvariant in productvariants
                        where productvariant.IsDeleted = false
                        select new ProductVariantDto 
                        {   
                            ProductId = productvariant.ProductId,
                            SizeId = productvariant.SizeId,
                            ColorId = productvariant.ColorId,
                            Price = productvariant.Price,
                            Quantity = productvariant.Quantity,
                            VariantId = productvariant.VariantId,
                            BrandId = productvariant.BrandId
                        }).ToList();
                    result.Message = "Product variants retrieved successfully";
                    return result;
                }
                result.Success = false;
                result.Message = "No Product variant found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantResponse> GetByIdAsync(int id)
        {
            ProductVariantResponse result = new();

            try
            {
                var productvariant = await _productVariantRepository.GetByIdAsync(id);

                if (productvariant != null)
                {
                    result.Data = new ProductVariantDto
                    {
                        ProductId = productvariant.ProductId,
                        SizeId = productvariant.SizeId,
                        ColorId = productvariant.ColorId,
                        Price = productvariant.Price,
                        Quantity = productvariant.Quantity,
                        VariantId = productvariant.VariantId,
                        BrandId = productvariant.BrandId
                    };

                    result.Message = "Product variant retrieved successfully";
                    return result;
                }
                result.Success = false;
                result.Message = "Product variant not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantResponse> SellAsync(SellVariantDto sellVariantDto)
        {
            ProductVariantResponse result = new ProductVariantResponse();
            try
            {
                var isValidToSell = await ProductVariantValidations.IsValidProductVariantToSell(sellVariantDto, _productVariantRepository);
                if (isValidToSell.Success)
                {

                    await _productVariantRepository.SellAsync(sellVariantDto.VariantId, sellVariantDto.Quantity);
                    result.Message = "Product variant sold successfully";
                    return result;
                }
                result.Success = false;
                result.Message = isValidToSell.Message;
                result.Data = isValidToSell.Data;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantResponse> UpdatePriceAsync(UpdatePriceDto updatePriceDto)
        {
            ProductVariantResponse result = new ProductVariantResponse();
            try
            {
                var isValidPriceToUpdate = await ProductVariantValidations.IsValidPriceToUpdate(updatePriceDto);
                if (isValidPriceToUpdate.Success)
                {
                    YukaDAL.Entities.ProductVariant productVariant = new()
                    {
                        VariantId = updatePriceDto.VariantId,
                        Price = updatePriceDto.Price,
                        UpdatedBy = updatePriceDto.UpdatedBy
                    };
                    await _productVariantRepository.UpdatePriceAsync(productVariant);   
                    result.Message = "Price updated successfully";
                    return result;
                }
                result.Success = false;
                result.Message = isValidPriceToUpdate.Message;
                result.Data = isValidPriceToUpdate.Data;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantUpdateResponse> UpdateProductVariantAsync(UpdateProductVariantDto updateProductVariantDto)
        {
            ProductVariantUpdateResponse result = new ProductVariantUpdateResponse();

            try
            {
                var productvariant = await _productVariantRepository.GetByIdAsync(updateProductVariantDto.ProductVariantId);

                if (productvariant != null)
                {
                    var isValidToUpdate = await ProductVariantValidations.IsValidProductVariantToUpdate(updateProductVariantDto, _productVariantRepository);
                    if (isValidToUpdate.Success)
                    {
                        YukaDAL.Entities.ProductVariant productVariant = new()
                        {
                            VariantId = updateProductVariantDto.ProductVariantId,
                            ProductId = updateProductVariantDto.ProductId,
                            SizeId = updateProductVariantDto.SizeId,
                            ColorId = updateProductVariantDto.ColorId,
                            UpdatedBy = updateProductVariantDto.UpdatedBy
                        };
                        await _productVariantRepository.UpdateAsync(productVariant);
                        result.Message = "Product variant updated successfully";
                        return result;
                    }
                    result.Success = false;
                    result.Message = isValidToUpdate.Message;
                    result.Data = isValidToUpdate.Data;
                    return result; 
                }
                result.Success = false;
                result.Message = "Product variant not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ProductVariantUpdateResponse> UpdateStockAsync(UpdateStockDto updateStockDto)
        {
            ProductVariantUpdateResponse result = new ProductVariantUpdateResponse();

            try
            {
                var isValidStockToUpdate = await ProductVariantValidations.IsValidStockToUpdate(updateStockDto);
                if (isValidStockToUpdate.Success)
                {
                    YukaDAL.Entities.ProductVariant productVariant = new()
                    {
                        VariantId = updateStockDto.ProductVariantId,
                        Quantity = updateStockDto.Stock,
                        UpdatedBy = updateStockDto.UpdatedBy
                    };
                    await _productVariantRepository.UpdateStockAsync(productVariant);
                    result.Message = "Stock updated successfully";
                    return result;

                }
                result.Success = false;
                result.Message = isValidStockToUpdate.Message;
                result.Data = isValidStockToUpdate.Data;
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
