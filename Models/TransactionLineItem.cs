namespace VendingAPI.Models
{
    public class TransactionLineItem
    {
        public long Id { get; set; }
        public long TransactionId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
