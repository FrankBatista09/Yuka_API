using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.ProductVariant
{
    public class UpdateProductVariantDto : UpdateDto
    {
        public int ProductVariantId { get; set; }
        public int BrandId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
    }
}
