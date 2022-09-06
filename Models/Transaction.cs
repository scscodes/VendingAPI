namespace VendingAPI.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public IEnumerable<TransactionLineItem> TransactionLineItem { get; set; }
        public long MachineId { get; set; }
    }
}