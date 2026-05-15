namespace apbd_cw7_git_s33418.DTO
{
    public class GetComponentsByID
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}
