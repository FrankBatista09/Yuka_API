using YukaBLL.Core.DtosBase;

namespace YukaBLL.Dtos.Color
{
    public class AddColorDto : AddDto
    {
        /// <summary>
        /// The name of the new color
        /// </summary>
        public string ColorName { get; set; }
    }
}
