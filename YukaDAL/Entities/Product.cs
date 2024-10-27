namespace YukaDAL.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }


        //Relations
        public ICollection<ProductVariant> ProductVariants { get; set; }
        public ICollection<ProductBrandPriceGroup> ProductBrandPriceGroups { get; set; }

    }
}
