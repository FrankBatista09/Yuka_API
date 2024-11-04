using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.ProductVariant
{
    public class UpdatePriceDto : UpdateDto
    {
        public int VariantId { get; set; }
        public double Price { get; set; }
    }
}
