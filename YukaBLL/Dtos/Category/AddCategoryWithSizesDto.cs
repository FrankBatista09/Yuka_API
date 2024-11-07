using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YukaBLL.Dtos.Category
{
    public class AddCategoryWithSizesDto
    {
        public string CategoryName { get; set; }
        public List<string> SelectedSizeIds { get; set; }
    }
}
