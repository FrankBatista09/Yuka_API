using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using YukaDAL.Context;
using YukaDAL.Entities;
using YukaDAL.Interfaces;

namespace YukaDAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly YukaContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(YukaContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Category> CreateAsync(Category entity)
        {
            try
            {
                if(entity == null)
                    throw new ArgumentNullException(nameof(entity), "The entity to create cannot be null.");

                await _context.Categories.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in CreateAsync for Category entity.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in CreateAsync for Category entity.");
                throw;
            }
        }

        public async Task DeleteAsync(Category entity)
        {
            try
            {
                if(entity == null)
                    throw new ArgumentNullException(nameof(entity), "The entity to delete cannot be null.");

                var category = await GetByIdAsync(entity.CategoryId);

                if (category == null)
                    throw new NullReferenceException("The entity to delete does not exist.");

                entity.DeletedDate = DateTime.Now;
                entity.DeletedBy = entity.DeletedBy;
                entity.IsDeleted = true;

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in DeleteAsync for Category entity.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in DeleteAsync for Category entity.");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Category, bool>> expression)
        {
            try
            {
                return await _context.Categories.AnyAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured on ExistsAsync");
                throw;
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured on GetAllAsync");
                throw;
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            try
            {
                if(id == null)
                    throw new ArgumentNullException(nameof(id), "The id to get the entity cannot be null.");

                return await _context.Categories.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured on GetByIdAsync");
                throw;
            }
        }

        public async Task UpdateAsync(Category entity)
        {
            try
            {
                var entityToUpdate = await GetByIdAsync(entity.CategoryId);

                if (entityToUpdate == null)
                    throw new NullReferenceException("The entity to update does not exist.");

                entityToUpdate.CategoryName = entity.CategoryName;
                entityToUpdate.UpdatedDate = DateTime.Now;
                entityToUpdate.UpdatedBy = entity.UpdatedBy;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured on UpdateAsync");
                throw;
            }
        }
    }
}
