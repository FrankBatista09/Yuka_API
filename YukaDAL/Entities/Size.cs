using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class Size : Entity
    {
        public int SizeId { get; set; }
        public required string SizeName { get; set; }

        //Relations
        public ICollection<SizeCategory> SizeCategories { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
