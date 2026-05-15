namespace apbd_cw7_git_s33418.Models
{
    public class ComponentManufacturer
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime FoundationDate { get; set; }
        public List<Component> Components { get; set; } = new();
    
    }
}
