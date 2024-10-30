using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface ISizeRepository : IBaseRepository<Size>
    {
        /// <summary>
        /// Gets the SizeGroups by the SizeName
        /// </summary>
        /// <param name="SizeName"></param>
        /// <returns>SizeGroup</returns>
        Task<SizeGroup> GetSizeGroupBySizeName(string SizeName);
    }
}
