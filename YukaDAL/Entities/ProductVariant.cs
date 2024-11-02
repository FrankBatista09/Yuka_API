using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class ProductVariant : Entity
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        //Relations
        public Product Product { get; set; }
        public Brand Brand { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
    }
}
