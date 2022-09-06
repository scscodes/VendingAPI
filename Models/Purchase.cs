namespace VendingAPI.Models
{
    public class Purchase
    {
        public long Id { get; set; }
        public Transaction Transaction { get; set; }
        public decimal AmountTendered { get; set; }
    }
}
