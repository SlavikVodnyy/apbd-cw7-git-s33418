namespace apbd_cw7_git_s33418.Models
{
    public class Component
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ComponentManufacturerId { get; set; }
        public int ComponentTypeId { get; set; }
        public ComponentManufacturer ComponentManufacturer { get; set; } = null!;
        public ComponentType ComponentType { get; set; } = null!;
        public List<PCComponent> PCComponents { get; set; } = new();
    }
}
