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
    public class ProductRepository : IProductRepository
    {
        private readonly YukaContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(YukaContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "The entity created cannot be null.");
                await _context.Products.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in CreateAsync for Product entity.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in CreateAsync for Product entity.");
                throw;
            }
        }

        public async Task DeleteAsync(Product entity)
        {
            try
            {
                var product = await GetByIdAsync(entity.ProductId);
                if (product == null)
                    throw new NullReferenceException("The entity to delete does not exist");

                product.DeletedBy = entity.DeletedBy;
                product.DeletedDate = DateTime.UtcNow;
                product.IsDeleted = true;

                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Datebase update error in DeleteAsync for Product entity.");
                throw;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in DeleteAsync for Product entity.");
                throw;
            }
        }

        public Task<bool> ExistsAsync(Expression<Func<Product, bool>> expression)
        {
            try
            {
                return _context.Products.AnyAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in ExistsAsync for Product entity.");
                throw;
            }
        }

        public async Task<List<Product>> GetAllAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllAsync for Product entity.");
                throw;
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "The Id to get cannot be null");

                return await _context.Products.FindAsync(id);

            }
            catch (NullReferenceException exn)
            {
                _logger.LogError(exn, "The Id to get does not exist");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllAsync for Product entity.");
                throw;
            }
        }

        public async Task UpdateAsync(Product entity)
        {
            try
            {
                var existingProduct = await GetByIdAsync(entity.ProductId);
                if (existingProduct == null)
                    throw new NullReferenceException("The entity to update does not exist");

                existingProduct.UpdatedDate = DateTime.UtcNow;
                existingProduct.UpdatedBy = existingProduct.UpdatedBy;
                existingProduct.ProductName = entity.ProductName;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in UpdateAsync for Product entity.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occured in UpdateAsync for Color entity");
            }
        }
    }
}
