namespace YukaDAL.Entities
{
    public class Brand
    {
        public int BrandId {  get; set; }
        public required string BrandName { get; set; }

        //Relations

        public ICollection<ProductBrandPriceGroup> ProductBrandPriceGroups { get; set; }
    }
}
