using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YukaDAL.Context;
using YukaDAL.Entities;
using YukaDAL.Interfaces;

namespace YukaDAL.Repositories
{
    public class SizeCategoryRepository : ISizeCategoryRepository
    {
        private readonly YukaContext _context;
        private readonly ILogger<SizeCategoryRepository> _logger;

        public SizeCategoryRepository(YukaContext yukaContext, ILogger<SizeCategoryRepository> logger)
        {
            _context = yukaContext;
            _logger = logger;
        }
        public async Task<SizeCategory> CreateAsync(SizeCategory entity)
        {
            try
            {
                if (entity == null) 
                    throw new ArgumentNullException(nameof(entity), "The entity created cannot be null.");
                await _context.SizeCategories.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch(DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in CreateAsync for SizeCategory entity.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in CreateAsync for SizeCategory entity");
                throw;
            }
        }

        public async Task DeleteAsync(SizeCategory entity)
        {
            try
            {
                var sizeCategory = await GetByIdAsync(entity.CategoryId);
                if (sizeCategory == null)
                    throw new NullReferenceException("The entity to delete does not exist");
                sizeCategory.DeletedDate = DateTime.UtcNow;
                sizeCategory.DeletedBy = entity.DeletedBy;
                sizeCategory.IsDeleted = true;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in DeleteAsync for SizeCategory entity.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in DeleteAsync for SizeCategory entity");
                throw;
            }
        }

        public Task<bool> ExistsAsync(Expression<Func<SizeCategory, bool>> expression)
        {
            try
            {
                return _context.SizeCategories.AnyAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in ExistsAsync for SizeCategory entity");
                throw;
            }
        }

        public async Task<List<SizeCategory>> GetAllAsync()
        {
            try
            {
                return await _context.SizeCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllAsync for SizeCategory entity");
                throw;
            }
        }

        public async Task<SizeCategory> GetByIdAsync(int id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "The Id to get cannot be null");
                return await _context.SizeCategories.FindAsync(id);
                
            }
            catch (NullReferenceException exn)
            {
                _logger.LogError(exn, "The Id to get does not exist");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetByIdAsync for SizeCategory entity");
                throw;
            }
        }

        public Task<List<Size>> SizeByCategory(int categoryID)
        {
            try
            {
                if (categoryID == null)
                    throw new ArgumentNullException(nameof(categoryID), "The Id to get cannot be null");
                return _context.SizeCategories
                    .Where(sc => sc.CategoryId == categoryID)
                    .Select(sc => sc.Size)
                    .Distinct()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in SizeByCategory for SizeCategory entity");
                throw;
            }
        }

        public async Task UpdateAsync(SizeCategory entity)
        {
            try
            {
                var existingSizeCategory = await GetByIdAsync(entity.SizeId);
                if (existingSizeCategory == null)
                    throw new NullReferenceException("The entity to update does not exist");

                existingSizeCategory.UpdatedDate = DateTime.UtcNow;
                existingSizeCategory.UpdatedBy = entity.UpdatedBy;
                existingSizeCategory.CategoryId = entity.CategoryId;
                existingSizeCategory.SizeId = entity.SizeId;

                await _context.SaveChangesAsync();  
                  
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in UpdateAsync for Product entity.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in UpdateAsync for SizeCategory entity");
                throw;
            }
        }
    }
}
