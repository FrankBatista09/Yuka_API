namespace YukaBLL.Exceptions.ProductVariant
{
    /// <summary>
    /// Thrown when the ammount to purchase is below or equal to zero.
    /// </summary>
    public class PurchaseBelowEqualsZeroException : Exception
    {
        /// <summary>
        /// Gets the ammount that caused the exception
        /// </summary>
        public int Purchase { get; set; }

        /// <summary>
        /// Creates an instance of the <see cref="PurchaseBelowEqualsZeroException"/> class.
        /// </summary>
        /// <param name="purchase">Ammount to substract from the quantity column from<see cref="YukaDAL.Entities.ProductVariant"/></param>
        public PurchaseBelowEqualsZeroException(int purchase) :
            base($"The ammount to purchase is below or equal to zero. Please enter a valid ammount.")
        {
            Purchase = purchase;
        }
    }
}
