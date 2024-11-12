using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class Product : Entity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }


        //Relations
        public Category Category { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }

    }
}
