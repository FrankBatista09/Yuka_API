using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface ISizeRepository : IBaseRepository<Size>
    {
        Task<Size> GetBySizeGroupId(int sizeGroupId);
    }
}
