namespace YukaBLL.Core.DtosBase
{
    public abstract class AddDto
    {
        /// <summary>
        /// User ID who created the record
        /// </summary>
        public int CreatedBy { get; set; }
    }
}
