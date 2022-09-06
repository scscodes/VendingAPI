namespace VendingAPI.Models
{
    public class Machine
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public MachineInventory MachineInventory { get; set; }
    }
}
