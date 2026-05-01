namespace priorizzeProject.Core.Models
{
    public class JiraSyncConfig
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string ProjectKey { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime? LastSync { get; set; }
    }
}
