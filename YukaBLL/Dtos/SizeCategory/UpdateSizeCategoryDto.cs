using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.SizeCategory
{
    public class UpdateSizeCategoryDto : UpdateDto
    {
        public int CategoryId { get; set; }
        public int SizeId { get; set; }
    }
}
