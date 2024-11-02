using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YukaDAL.Entities;
using YukaDAL.Interfaces;

namespace YukaDAL.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        public Task<Brand> CreateAsync(Brand entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<Brand, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<List<Brand>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Brand> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
