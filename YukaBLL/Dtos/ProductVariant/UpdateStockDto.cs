using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.ProductVariant
{
    public class UpdateStockDto : UpdateDto
    {
        public int ProductVariantId { get; set; }
        public int Stock { get; set; }
    }
}
