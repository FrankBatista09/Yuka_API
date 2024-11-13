using Microsoft.Extensions.Logging;
using YukaBLL.Contracts;
using YukaBLL.Dtos.Product;
using YukaBLL.Dtos.ProductVariant;
using YukaBLL.Responses.ProductVariant;
using YukaBLL.Validations.ProductVariant;
using YukaDAL.Entities;
using YukaDAL.Interfaces;

namespace YukaBLL.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ILogger<ProductVariantService> _logger;
        private readonly ISizeRepository _sizeRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IProductRepository _productRepository;

        public ProductVariantService(IProductVariantRepository productVariantRepository, ILogger<ProductVariantService> logger,
            ISizeRepository sizeRepository, IBrandRepository brandRepository, IColorRepository colorRepository, IProductRepository productRepository)
        {
            _productVariantRepository = productVariantRepository;
            _logger = logger;
            _sizeRepository = sizeRepository;
            _brandRepository = brandRepository;
            _colorRepository = colorRepository;
            _productRepository = productRepository;
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
                            CreatedDate = DateTime.UtcNow,
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
                var stockToUpdate = await _productVariantRepository.GetByIdAsync(addToStockDto.ProductVariantId);
                if (stockToUpdate != null)
                {
                    var isValidStock = await ProductVariantValidations.IsValidStockToAdd(addToStockDto, _productVariantRepository);
                    if (isValidStock.Success)
                    {
                        YukaDAL.Entities.ProductVariant productVariant = new()
                        {
                            VariantId = addToStockDto.ProductVariantId,
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
                var brands = await _brandRepository.GetAllAsync();
                var products = await _productRepository.GetAllAsync();
                var sizes = await _sizeRepository.GetAllAsync();
                var colors = await _colorRepository.GetAllAsync();

                if (productvariants.Any())
                {
                    result.Data = (
                        from productvariant in productvariants
                        join brand in brands on productvariant.BrandId equals brand.BrandId
                        join product in products on productvariant.ProductId equals product.ProductId
                        join size in sizes on productvariant.SizeId equals size.SizeId
                        join color in colors on productvariant.ColorId equals color.ColorId
                        where productvariant.IsDeleted = false
                        orderby product.ProductName, brand.BrandName, color.ColorName, size.SizeName
                        select new ProductVariantDto
                        {
                            VariantId = productvariant.VariantId,
                            ProductId = productvariant.ProductId,
                            ProductName = product.ProductName,
                            BrandId = productvariant.BrandId,
                            BrandName = brand.BrandName,    
                            ColorId = productvariant.ColorId,
                            ColorName = color.ColorName,
                            SizeId = productvariant.SizeId,
                            SizeName = size.SizeName,
                            Price = productvariant.Price,
                            Quantity = productvariant.Quantity,                
                          
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
                var product = await _productRepository.GetByIdAsync(productvariant.ProductId);
                var brand = await _brandRepository.GetByIdAsync(productvariant.BrandId);
                var size = await _sizeRepository.GetByIdAsync(productvariant.SizeId);
                var color = await _colorRepository.GetByIdAsync(productvariant.BrandId);
                if (productvariant != null)
                {
                    result.Data = new ProductVariantDto
                    {
                        VariantId = productvariant.VariantId,
                        ProductId = productvariant.ProductId,
                        ProductName = product.ProductName,
                        BrandId = productvariant.BrandId,
                        BrandName = brand.BrandName,
                        ColorId = productvariant.ColorId,
                        ColorName = color.ColorName,
                        SizeId = productvariant.SizeId,
                        SizeName = size.SizeName,
                        Price = productvariant.Price,
                        Quantity = productvariant.Quantity
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
                            BrandId = updateProductVariantDto.BrandId,
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
