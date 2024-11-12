using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.Category
{
    public class AddCategoryWithSizesDto : AddDto
    {
        public string CategoryName { get; set; }
        public List<int> SelectedSizeIds { get; set; }
    }
}
