namespace VendingAPI.Models
{
    public class MachineInventory
    {
        public long Id { get; set; }
        public IEnumerable<MachineInventoryLineItem> MachineInventoryLineItem { get; set; }
    }
}
