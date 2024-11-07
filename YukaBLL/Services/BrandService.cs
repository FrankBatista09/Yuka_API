using Microsoft.Extensions.Logging;
using YukaBLL.Contracts;
using YukaBLL.Dtos.Brand;
using YukaBLL.Responses.Brand;
using YukaBLL.Validations.BrandValidations;
using YukaDAL.Interfaces;

namespace YukaBLL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ILogger<BrandService> _logger;

        public BrandService(IBrandRepository brandRepository, ILogger<BrandService> logger)
        {
            _brandRepository = brandRepository;
            _logger = logger;
        }

        public async Task<BrandAddResponse> AddBrandAsync(AddBrandDto addBrandDto)
        {
            BrandAddResponse result = new();
            try
            {

                var isValidBrand = await BrandValidations.IsValidBrandToAdd(addBrandDto, _brandRepository);

                if (isValidBrand.Success) 
                {
                    YukaDAL.Entities.Brand brand = new()
                    {
                        BrandName = addBrandDto.BrandName,
                        CreatedBy = addBrandDto.CreatedBy
                    };
                    result.Data = await _brandRepository.CreateAsync(brand);
                    result.Message = "Brand added successfully";
                    return result;
                }
                result.Success = false;
                result.Message = isValidBrand.Message;
                result.Data = isValidBrand.Data;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<BrandDeleteResponse> DeleteBrandAsync(DeleteBrandDto deleteBrandDto)
        {
            BrandDeleteResponse result = new();

            try
            {
                YukaDAL.Entities.Brand brand = new()
                {
                    BrandId = deleteBrandDto.BrandId,
                    DeletedBy = deleteBrandDto.DeletedBy,
                };
                await _brandRepository.DeleteAsync(brand);

                result.Message = "Brand deleted successfully";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<BrandResponse> GetAllAsync()
        {
            BrandResponse result = new();
            try
            {
                var brands = await _brandRepository.GetAllAsync();

                if (brands.Any())
                {
                    result.Data = (
                                from brand in brands
                                where brand.IsDeleted == false
                                select new BrandDto
                                {
                                    BrandId = brand.BrandId,
                                    BrandName = brand.BrandName
                                }).ToList();

                    result.Message = "Brands retrieved successfully";
                    return result; 
                }
                result.Success = false;
                result.Message = "No brands found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            } 
            
        }

        public async Task<BrandResponse> GetByIdAsync(int id)
        {
            BrandResponse result = new();

            try
            {
                var brand = await _brandRepository.GetByIdAsync(id);

                if (brand != null)
                {
                    result.Data = new BrandDto
                    {
                        BrandId = brand.BrandId,
                        BrandName = brand.BrandName
                    };

                    result.Message = "Brand retrieved successfully";
                    return result;
                }
                result.Success = false;
                result.Message = "Brand not found";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<BrandUpdateResponse> UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            BrandUpdateResponse result = new();

            try
            {
                var IsValidToUpdate = await BrandValidations.IsValidBrandToUpdate(updateBrandDto, _brandRepository);

                if (IsValidToUpdate.Success)
                {
                    YukaDAL.Entities.Brand brand = new()
                    {
                        BrandId = updateBrandDto.BrandId,
                        BrandName = updateBrandDto.BrandName,
                        UpdatedBy = updateBrandDto.UpdatedBy,
                    };

                    await _brandRepository.UpdateAsync(brand);

                    result.Message = "Brand updated successfully";
                    return result;
                }
                result.Success=false;
                result.Message = IsValidToUpdate.Message;
                result.Data = IsValidToUpdate.Data;
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
