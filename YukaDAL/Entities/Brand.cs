using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class Brand : Entity
    {
        public int BrandId {  get; set; }
        public required string BrandName { get; set; }

        //Relations
        public ICollection<ProductVariant> ProductVariants { get; set; }

    }
}
