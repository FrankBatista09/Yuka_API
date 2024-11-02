using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.Brand
{
    public class UpdateBrandDto : UpdateDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

    }
}
