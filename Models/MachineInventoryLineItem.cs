namespace VendingAPI.Models
{
    public class MachineInventoryLineItem
    {
        public long Id { get; set; }
        public Product Product { get; set; }
        public int CurrentQuantity { get; set; }
    }
}
