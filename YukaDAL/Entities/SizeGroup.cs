using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class SizeGroup : Entity
    {
        public int SizeGroupId { get; set; }
        public string SizeGroupName { get; set; }
        

        //Relations
        public ICollection<Size> Sizes { get; set; }
        public ICollection<ProductBrandPriceGroup> ProductBrandPriceGroups { get; set; }
    }
}
