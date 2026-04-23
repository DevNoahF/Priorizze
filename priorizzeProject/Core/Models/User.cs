using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Models
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public string? SquadName { get; private set; }
        public string? SquadJiraKey { get; private set; }
        public string? JiraTeamId { get; private set; }
        public string JiraAccountId { get; private set; } = string.Empty;
        public string JiraEmail { get; private set; } = string.Empty;
        public string JiraApiTokenEnc { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public ICollection<JiraSyncConfig> JiraSyncConfigs { get; private set; } = [];

        public User()
        {
        }

        public User(
            string name,
            string email,
            UserRole role,
            string jiraAccountId,
            string jiraEmail,
            string jiraApiTokenEnc,
            string? squadName = null,
            string? squadJiraKey = null,
            string? jiraTeamId = null)
        {
            Name = name;
            Email = email;
            Role = role;
            JiraAccountId = jiraAccountId;
            JiraEmail = jiraEmail;
            JiraApiTokenEnc = jiraApiTokenEnc;
            SquadName = squadName;
            SquadJiraKey = squadJiraKey;
            JiraTeamId = jiraTeamId;
        }

    }
}
