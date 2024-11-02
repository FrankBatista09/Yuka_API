using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class Category : Entity
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }

        //Relations
        public ICollection<SizeCategory> SizeCategories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
