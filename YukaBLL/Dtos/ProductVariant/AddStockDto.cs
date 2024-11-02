using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.ProductVariant
{
    public class AddStockDto : UpdateDto
    {
        public int ProductVariantId { get; set; }
        public int StockToAdd { get; set; }
    }
}
