using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.SizeCategory
{
    public class DeleteSizeCategoryDto : DeleteDto
    {
        public int CategoryId { get; set; }
        public int SizeId { get; set; }
    }
}
