namespace YukaDAL.Entities
{
    public class Size
    {
        public int SizeId { get; set; }
        public required string SizeName { get; set; }
        public int SizeGroupId { get; set; }

        //Relations
        public SizeGroup SizeGroup { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
