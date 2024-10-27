namespace YukaDAL.Entities
{
    public class ProductBrandPriceGroup
    {
        public int PriceGroupId { get; set; }
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public int SizeGroupId { get; set; }
        public bool IsWhite { get; set; }
        public decimal Price {  get; set; }

        //Relations
        public Brand Brand { get; set; }
        public Product Product { get; set; }
        public SizeGroup SizeGroup { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
