using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.Size
{
    public class UpdateSizeDto : UpdateDto
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }
    }
}
