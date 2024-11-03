using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.Category
{
    public class UpdateCategoryDto : UpdateDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
