using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using YukaDAL.Context;
using YukaDAL.Entities;
using YukaDAL.Interfaces;

namespace YukaDAL.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        //Fields
        private readonly YukaContext _context;
        private readonly ILogger<SizeRepository> _logger;

        //Dependency Injection
        public SizeRepository(YukaContext context, ILogger<SizeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Size> CreateAsync(Size entity)
        {
            try
            {
                //Case entity is null throw an early exception increasing speed
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "The entity to create cannot be null.");

                await _context.Sizes.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            //Case of database update error while saving the entity
            catch (DbUpdateException upEx)
            {
                _logger.LogError(upEx, "Database update error in CreateAsync for Size entity.");
                throw;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in CreateAsync for Size entity.");
                throw;
            }

        }

        public async Task<bool> ExistsAsync(Expression<Func<Size, bool>> expression)
        {
            //No need to implement try catch because Any will throw an exception if it fails
            try
            {
                return await _context.Sizes.AnyAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in ExistsAsync for Size entity.");
                throw;
            }
        }

        public async Task<List<Size>> GetAllAsync()
        {
            try
            {
                return await _context.Sizes.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllAsync for Size entity.");
                throw;
            }
        }

        public async Task<Size> GetByIdAsync(int id)
        {
            try
            {
                //Early return of null exception if id is null
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "The id to get the entity cannot be null.");

                return await _context.Sizes.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetByIdAsync for Size entity.");
                throw;
            }
        }

        public async Task<SizeGroup> GetSizeGroupBySizeName(string SizeName)
        {
            try
            {
                //Early return of null exception if SizeName is null
                if (SizeName == null)
                    throw new ArgumentNullException(nameof(SizeName), "The SizeName to get the SizeGroup cannot be null.");
                
                return await _context.Sizes
                    .Where(s => s.SizeName == SizeName) //Filter by SizeName
                    .Select(s => s.SizeGroup) //Select the SizeGroup using the navigation property
                    .FirstOrDefaultAsync(); //Get the first result or null
            }
            //Case that the Size Group could not be found by the SizeName
            catch (NullReferenceException exnr)
            {
                _logger.LogError(exnr, "The SizeGroup to get does not exist.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetSizeGroupBySizeName for Size entity.");
                throw;
            }
        }
    }
}
