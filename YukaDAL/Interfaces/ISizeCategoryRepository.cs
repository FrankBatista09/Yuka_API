﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaDAL.Core;
using YukaDAL.Entities;

namespace YukaDAL.Interfaces
{
    public interface ISizeCategoryRepository : IBaseRepository<SizeCategory>
    {
        /// <summary>
        /// Get sizes by category
        /// </summary>
        /// <param name="categoryID">The Id of the category</param>
        /// <returns>List of the sizes that belong to that category</returns>
        Task<List<Size>> SizeByCategory(int categoryID);
    }
}
