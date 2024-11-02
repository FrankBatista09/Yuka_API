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
    public class BrandRepository : IBrandRepository
    {
        //Fields
        private readonly YukaContext _context;
        private readonly ILogger<BrandRepository> _logger;

        //Dependency Injection
        public BrandRepository(YukaContext context, ILogger<BrandRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Brand> CreateAsync(Brand entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity to create cannot be null.");

            try
            {
                await _context.Brands.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in CreateAsync for Brand entity.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in CreateAsync for Brand entity.");
                throw;
            }
        }


        public async Task<bool> ExistsAsync(Expression<Func<Brand, bool>> expression)
        {
            //No need to use try-catch block here, because AnyAsync will throw an exception if it fails
            try
            {
                return await _context.Brands.AnyAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in ExistsAsync for Brand entity.");
                throw;
            }
        }

        public async Task<List<Brand>> GetAllAsync()
        {
            try
            {
                return await _context.Brands.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllAsync for Brand entity.");
                throw;
            }
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Brands.FindAsync(id);
            }
            //Case that the id does not exist
            catch (NullReferenceException exnr)
            {
                _logger.LogError(exnr, "The id to get does not exist.");
                throw;
            }
            //Case that the id is null
            catch (ArgumentNullException exn)
            {
                _logger.LogError(exn, "The id to get cannot be null.");
                throw;
            }
            //Case not controlled
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetByIdAsync for Brand entity.");
                throw;
            }

        }
    }
}
