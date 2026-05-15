namespace apbd_cw7_git_s33418.Models
{
    public class PC
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Weight { get; set; }
        public int Warranty { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Stock { get; set; }
        public List<PCComponent> PCComponents { get; set; } = new();
    }
}
