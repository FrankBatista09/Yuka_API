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
    public class ColorRepository : IColorRepository
    {
        private readonly YukaContext _context;
        private readonly ILogger<ColorRepository> _logger;

        public ColorRepository(YukaContext yukaContext, ILogger<ColorRepository> logger)
        {
            _context = yukaContext;
            _logger = logger;
        }
        public async Task<Color> CreateAsync(Color entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity created cannot be null.");
            try
            {
                await _context.Colors.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in CreateAsync for Color entity.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in CreateAsyn for Color entity");
                throw;
            }
        }

        public async Task DeleteAsync(Color entity)
        {
            try
            {
                var color = await GetByIdAsync(entity.ColorId);
                if (color == null)
                    throw new NullReferenceException("The entity to delete does not exist");
                color.DeletedDate = DateTime.UtcNow;
                color.DeletedBy = entity.DeletedBy;
                color.IsDeleted = true;

                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in DeleteAsync for Color entity");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Color, bool>> expression)
        {
            try
            {
                return await _context.Colors.AnyAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in ExistsAsyn for Color entity");
                throw;
            }
        }

        public Task<List<Color>> GetAllAsync()
        {
            try
            {
                return _context.Colors.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllAsync for Color Entity");
                throw;
            }
        }

        public async Task<Color> GetByIdAsync(int id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "The Id to get cannot be null.");
                return await _context.Colors.FindAsync(id);
            }
            catch (NullReferenceException exn) 
            {
                _logger.LogError(exn, "The id to get does not exist.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetByIdAsync for Color entity");
                throw;
            }
        }

        public async Task UpdateAsync(Color entity)
        {
            try
            {
                var existingColor = await GetByIdAsync(entity.ColorId);
                if (existingColor == null)
                    throw new NullReferenceException("The entity to Update does not exist");

                existingColor.UpdatedBy = entity.UpdatedBy;
                existingColor.UpdatedDate = DateTime.UtcNow; 
                existingColor.ColorName = entity.ColorName; 

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occured in UpdateAsync for Color entity");
            }
        }
    }
}
