namespace YukaDAL.Core
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
    }
}
