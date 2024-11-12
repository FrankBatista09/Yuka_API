using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class Color : Entity
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    
        //Relations
        public ICollection<ProductVariant> ProductVariants { get; set; }

    }
}
