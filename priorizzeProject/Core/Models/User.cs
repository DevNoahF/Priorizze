using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? SquadName { get; set; }
        public string? SquadJiraKey { get; set; }
        public string? JiraTeamId { get; set; }
        public string JiraAccountId { get; set; } = string.Empty;
        public string JiraEmail { get; set; } = string.Empty;
        public string JiraApiTokenEnc { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<JiraSyncConfig> JiraSyncConfigs { get; set; } = [];
    }
}
