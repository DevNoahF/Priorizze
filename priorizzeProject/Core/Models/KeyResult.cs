namespace priorizzeProject.Core.Models
{
    public class KeyResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OkrId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal InitialValue { get; set; }
        public decimal GoalValue { get; set; }
        public decimal CurrentValue { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateTime LimitDate { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
