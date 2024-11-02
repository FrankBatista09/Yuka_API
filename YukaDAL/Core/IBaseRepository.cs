using System.Linq.Expressions;

namespace YukaDAL.Core
{
    public interface IBaseRepository<TEntity> where TEntity : Entity 
    {
        /// <summary>
        /// Generic method to create a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The entity created</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Generic Method to get all entities
        /// </summary>
        /// <returns>A List of the entity</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Generic Method to get x entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The entity found</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Generic Method to get certain entity by an expression
        /// </summary>
        /// <param name="expression">Lambda expression indicating the conditions for the entity</param>
        /// <returns>A bool indicating wether the entity exists or not</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}
