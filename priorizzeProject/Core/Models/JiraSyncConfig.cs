namespace priorizzeProject.Core.Models
{
    public class JiraSyncConfig
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public User? User { get; private set; }
        public string ProjectKey { get; private set; } = string.Empty;
        public string Url { get; private set; } = string.Empty;
        public DateTime? LastSync { get; private set; }

        public JiraSyncConfig()
        {
        }

        public JiraSyncConfig(Guid userId, string projectKey, string url, DateTime? lastSync = null)
        {
            UserId = userId;
            ProjectKey = projectKey;
            Url = url;
            LastSync = lastSync;
        }

        public void MarkSynced(DateTime syncTime)
        {
            LastSync = syncTime;
        }

    }
}
