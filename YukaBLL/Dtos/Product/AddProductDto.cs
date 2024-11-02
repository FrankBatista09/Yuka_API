using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.Product
{
    public class AddProductDto : AddDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
    }
}
