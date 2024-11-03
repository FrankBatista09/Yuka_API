namespace YukaBLL.Core
{
    public interface IBaseService<T> where T : ServiceResult
    {
        /// <summary>
        /// Generic Method to get all entities
        /// </summary>
        /// <returns>A ServiceResult including a List of the entity</returns>
        Task<T> GetAllAsync();

        /// <summary>
        /// Generic Method to get x entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A ServiceResult with The entity found</returns>
        Task<T> GetByIdAsync(int id);
    }
}
