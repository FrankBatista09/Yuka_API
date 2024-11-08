using Microsoft.Extensions.Logging;
using YukaBLL.Contracts;
using YukaBLL.Dtos.SizeCategory;
using YukaBLL.Responses.Size;
using YukaBLL.Responses.SizeCategory;
using YukaDAL.Interfaces;


namespace YukaBLL.Services
{
    public class SizeCategoryService : ISizeCategoryService
    {
        private readonly ISizeCategoryRepository _sizeCategoryRepository;
        private readonly ILogger<SizeCategoryService> _logger;

        public SizeCategoryService(ISizeCategoryRepository sizeCategoryRepository, ILogger<SizeCategoryService> logger)
        {
            _sizeCategoryRepository = sizeCategoryRepository;
            _logger = logger;
        }
        public Task<SizeCategoryAddResponse> AddSizeCategoryAsync(AddSizeCategoryDto addSizeCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<SizeCategoryDeleteResponse> DeleteSizeCategoryAsync(DeleteSizeCategoryDto deleteSizeCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<SizeCategoryResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SizeCategoryResponse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SizeResponse> GetSizesByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<SizeCategoryUpdateResponse> UpdateSizeCategoryAsync(UpdateSizeCategoryDto updateSizeCategoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
