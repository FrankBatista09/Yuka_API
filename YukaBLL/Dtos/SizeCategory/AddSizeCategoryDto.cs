using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.SizeCategory
{
    public class AddSizeCategoryDto : AddDto
    {
        public int CategoryId { get; set; }
        public int SizeId { get; set; }
    }
}
