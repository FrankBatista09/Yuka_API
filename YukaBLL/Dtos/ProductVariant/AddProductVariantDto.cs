using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.ProductVariant
{
    public class AddProductVariantDto : AddDto
    {
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
