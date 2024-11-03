using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukaDAL.Core;

namespace YukaDAL.Entities
{
    public class SizeCategory : Entity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SizeId { get; set; }
        public Size Size { get; set; }
    }
}
