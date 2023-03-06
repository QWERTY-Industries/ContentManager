namespace ContentManager.Models
{
    public class User
    {
        public string id { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public bool admin { get; set; }
    }
}
