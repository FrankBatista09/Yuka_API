using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.Product
{
    public class UpdateProductDto : UpdateDto 
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
    }
}
