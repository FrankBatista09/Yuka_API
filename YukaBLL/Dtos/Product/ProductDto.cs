namespace YukaBLL.Dtos.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
