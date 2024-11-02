using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class Color : Entity
    {
        public int ColorId { get; set; }
        public required string ColorName { get; set; }
    
        //Relations

        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
