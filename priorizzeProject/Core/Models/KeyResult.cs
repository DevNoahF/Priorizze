namespace priorizzeProject.Core.Models
{
    public class KeyResult
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid OkrId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal InitialValue { get; private set; }
        public decimal GoalValue { get; private set; }
        public decimal CurrentValue { get; private set; }
        public string Unit { get; private set; } = string.Empty;
        public DateTime LimitDate { get; private set; }
        public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;

        public KeyResult()
        {
        }

        public KeyResult(
            Guid okrId,
            string title,
            decimal initialValue,
            decimal goalValue,
            decimal currentValue,
            string unit,
            DateTime limitDate,
            string? description = null)
        {
            OkrId = okrId;
            Title = title;
            Description = description;
            InitialValue = initialValue;
            GoalValue = goalValue;
            CurrentValue = currentValue;
            Unit = unit;
            LimitDate = limitDate;
        }

        public void UpdateCurrentValue(decimal currentValue)
        {
            CurrentValue = currentValue;
            LastUpdated = DateTime.UtcNow;
        }

    }
}
