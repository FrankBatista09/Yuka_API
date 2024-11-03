using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.Color
{
    public class UpdateColorDto : UpdateDto
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
}
