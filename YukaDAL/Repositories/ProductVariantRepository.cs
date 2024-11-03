using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using YukaDAL.Context;
using YukaDAL.Entities;
using YukaDAL.Interfaces;

namespace YukaDAL.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly YukaContext _context;
        private readonly ILogger<ProductVariantRepository> _logger;
        public ProductVariantRepository(YukaContext yukaContext, ILogger<ProductVariantRepository> logger)
        {
            _context = yukaContext;
            _logger = logger;
        }
        public async Task AddToStock(ProductVariant entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "The entity cannot be null");
                if (entity.Quantity < 0)
                    throw new ArgumentOutOfRangeException(nameof(entity.Quantity), "The amount to be increased cannot be less than 0.");

                var productVariant = await GetByIdAsync(entity.VariantId);

                productVariant.Quantity += entity.Quantity;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Databa Update error occurred in AddToStock for ProductVariant entity");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in AddToStock for ProductVariant entity.");
                throw;
            }
            
        }

        public async Task BulkCreateAsync(List<ProductVariant> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities), "The list of entities cannot be null");  
                await _context.ProductVariants.AddRangeAsync(entities);
                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database Update error in BulkCreateAsync for ProductVariant entity.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in BulkCreateAsync for ProductVariant entity.");
            }
        }

        public async Task<ProductVariant> CreateAsync(ProductVariant entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "The entity created cannot be null.");
                await _context.ProductVariants.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database Update error in CreateAsync for ProductVariant entity");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected occurred in CreateAsync for ProductVariant entity.");
                throw;
            }
        }

        public async Task DeleteAsync(ProductVariant entity)
        {
            try
            {
                var productVariant = await GetByIdAsync(entity.VariantId);
                if (productVariant == null)
                    throw new ArgumentNullException(nameof(entity), "The entity to delete cannot be null");

                productVariant.DeletedBy = entity.DeletedBy;
                productVariant.DeletedDate = DateTime.UtcNow;
                productVariant.IsDeleted = true;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database Update error occurred in DeleteAsync for ProductVariant");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected occurred in DeleteAsync for ProductVariant entity");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<ProductVariant, bool>> expression)
        {
            try
            {
                return await _context.ProductVariants.AnyAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected occurred in ExistsAsync for ProductVariant entity");
                throw;
            }
        }

        public async Task<List<ProductVariant>> GetAllAsync()
        {
            try
            {
                return await _context.ProductVariants.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected occurred in GetAllAsync for ProductVariant entity");
                throw;
            }
        }

        public async Task<ProductVariant> GetByIdAsync(int id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "The Id to get cannot be null");
                return await _context.ProductVariants.FindAsync(id);
            }
            catch (NullReferenceException exn)
            {
                _logger.LogError(exn, "The Id to get does not exist");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected occurred in GetByIdAsync for ProductVariant entity");
                throw;
            }
        }

        public async Task<List<ProductVariant>> GetBySpecificAsync(Expression<Func<ProductVariant, bool>> expression)
        {
            try
            {
                return await _context.ProductVariants
                    .Where(expression)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected occurred in GetBySpecificAsync for ProductVariant entity");
                throw;
            }
        }

        public async Task SellAsync(int id, int quantity)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "The id cannot be null");
                if(quantity < 0)
                    throw new ArgumentOutOfRangeException(nameof(quantity), "The quantity to be sold cannot be lower than 0.");

                var productVariant = await GetByIdAsync(id);
                if (productVariant.Quantity - quantity < 0)
                    throw new ArgumentOutOfRangeException(nameof(quantity), "The quantity to be sold may not exceed the stock.");

                productVariant.Quantity -= quantity;

                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error in SellAsync for ProductVariant entity.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in SellAsync for ProductVariant entity.");
            }
        }

        public async Task UpdateAsync(ProductVariant entity)
        {
            try
            {
                var existingProductVariant = await _context.ProductVariants.FindAsync(entity.VariantId);
                if (existingProductVariant == null)
                    throw new NullReferenceException("The entity to update does not exist");

                existingProductVariant.BrandId = entity.BrandId;
                existingProductVariant.ProductId = entity.ProductId;
                existingProductVariant.ColorId = entity.ColorId;
                existingProductVariant.SizeId = entity.SizeId;
                existingProductVariant.UpdatedBy = entity.UpdatedBy;
                existingProductVariant.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database Update error in UpdateAsync for ProductVariant entity");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred ");
            }
        }

        public async Task UpdatePriceAsync(ProductVariant entity)
        {
            try
            {
                if (entity.Price < 0)
                    throw new ArgumentOutOfRangeException(nameof(entity.Price), "The new price cannot be lower than 0");

                var existingPrice = await GetByIdAsync(entity.VariantId);
                if (existingPrice == null)
                    throw new NullReferenceException("The Id cannot be null");

                existingPrice.Price = entity.Price;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database Update error in UpdatePriceAsync for ProductVariant entity");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in UpdatePriceAsync for ProductVariant");
                throw;
            }
        }

        public async Task UpdateStockAsync(ProductVariant entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "The entity cannot be null");
                if (entity.Quantity < 0)
                    throw new ArgumentOutOfRangeException(nameof(entity.Quantity), "The new stock cannot be lower than 0");

                var updatedStock = await GetByIdAsync(entity.VariantId);

                updatedStock.Quantity = entity.Quantity;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database Update error in UpdatePriceAsync for ProductVariant entity");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in UpdateStockAsync for ProductVariant entity.");
                throw;
            }
        }
    }
}
