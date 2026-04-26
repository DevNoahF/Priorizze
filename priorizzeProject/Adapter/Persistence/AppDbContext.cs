using Microsoft.EntityFrameworkCore;
using priorizzeProject.Core.Models;

namespace priorizzeProject.Adapter.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // TODO (para lembrar):
    // DbSet é uma coleção que representa uma tabela no banco de dados.
    // Ele permite que você faça operações CRUD (Create, Read, Update, Delete) com os dados.

    public DbSet<Cycle> Cycles { get; set; }

    public DbSet<MetricsHistory> MetricsHistories { get; set; }

    public DbSet<JiraProjects> JiraProjects { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<KeyResult> KeyResults { get; set; }

    public DbSet<JiraSyncConfig> JiraSyncConfigs { get; set; }
    
    public DbSet<OKRs> Okrs { get; set; }
    
    public DbSet<JiraTasks> JiraTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureCycleEntity(modelBuilder);

        ConfigureMetricsHistoryEntity(modelBuilder);

        ConfigureJiraProjectsEntity(modelBuilder);

        ConfigureUserEntity(modelBuilder);

        ConfigureKeyResultEntity(modelBuilder);

        ConfigureJiraSyncConfigEntity(modelBuilder);
        
        ConfigureOKRsEntity(modelBuilder);
        ConfigureJiraTasksEntity(modelBuilder);
    }

private static void ConfigureOKRsEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OKRs>(entity =>
        {
            // Chave primária
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.CycleId)
                .IsRequired();

            entity.Property(e => e.ManagerId)
                .IsRequired();

            // ManagerId é opcional (nullable = todos)
            entity.Property(e => e.ManagerId);

            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasComment("Status do OKR (1=Ativo, 2=Pausado, 3=Concluido, 4=Criado, 5=Cancelado)");

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.ToTable("OKRs");
        });
    }

    private static void ConfigureJiraTasksEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JiraTasks>(entity =>
        {
            // Chave primária
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Índice único para garantir que a key externa do Jira não seja duplicada no banco
            entity.HasIndex(e => e.ExternalKey)
                .IsUnique();

            entity.Property(e => e.ExternalKey)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Summary)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ProjectId)
                .IsRequired();

            entity.Property(e => e.SquadId)
                .IsRequired();

            entity.Property(e => e.KpiId);

            entity.Property(e => e.IssueType)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.PriorityName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.StoryPoints);

            entity.Property(e => e.AssigneeAccountId)
                .HasMaxLength(128);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.ResolvedAt);

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            entity.ToTable("Jira_Tasks");
        });
    }

    private static void ConfigureCycleEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cycle>(entity =>
        {
            // Chave primária
            entity.HasKey(e => e.Id);

            // Propriedade: Id
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("ID único do ciclo");

            // Propriedade: CyclesEnum (Q1, Q2, Q3, Q4)
            entity.Property(e => e.CyclesEnum)
                .IsRequired()
                .HasConversion<int>()
                .HasComment("Trimestre do ano (1=Q1, 2=Q2, 3=Q3, 4=Q4)");

            // Propriedade: Year
            entity.Property(e => e.Year)
                .IsRequired()
                .HasComment("Ano do ciclo");

            // Nome da tabela
            entity.ToTable("Cycles", t => t.HasComment("Tabela de ciclos/trimestres"));
        });
    }

    private static void ConfigureMetricsHistoryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MetricsHistory>(entity =>
        {
            // Chave primária
            entity.HasKey(e => e.Id);

            // Propriedade: Id
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Propriedade: TargetId
            entity.Property(e => e.TargetId)
                .IsRequired();

            // Propriedade: TypesEnum
            entity.Property(e => e.TypesEnum)
                .IsRequired()
                .HasConversion<int>();

            // Propriedade: CapturedValue
            entity.Property(e => e.CapturedValue)
                .IsRequired();

            // Propriedade: RecordedAt
            entity.Property(e => e.RecordedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.ToTable("MetricsHistories");
        });
    }

    private static void ConfigureJiraProjectsEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JiraProjects>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.JiraId)
                .IsRequired()
                .HasMaxLength(64);

            entity.Property(e => e.ProjectKey)
                .IsRequired()
                .HasMaxLength(32);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.JiraUrl)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.LastSync)
                .IsRequired(false);

            entity.ToTable("JiraProjects");
        });
    }

    private static void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Role)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(e => e.SquadName)
                .HasMaxLength(150);

            entity.Property(e => e.SquadJiraKey)
                .HasMaxLength(64);

            entity.Property(e => e.JiraTeamId)
                .HasMaxLength(128);

            entity.Property(e => e.JiraAccountId)
                .IsRequired()
                .HasMaxLength(64);

            entity.Property(e => e.JiraEmail)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.JiraApiTokenEnc)
                .IsRequired()
                .HasMaxLength(512);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasMany(e => e.JiraSyncConfigs)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.ToTable("Users");
        });
    }

    private static void ConfigureKeyResultEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeyResult>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.OkrId)
                .IsRequired();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.InitialValue)
                .IsRequired();

            entity.Property(e => e.GoalValue)
                .IsRequired();

            entity.Property(e => e.CurrentValue)
                .IsRequired();

            entity.Property(e => e.Unit)
                .IsRequired()
                .HasMaxLength(32);

            entity.Property(e => e.LimitDate)
                .IsRequired();

            entity.Property(e => e.LastUpdated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.ToTable("KeyResults");
        });
    }

    private static void ConfigureJiraSyncConfigEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JiraSyncConfig>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId)
                .IsRequired();

            entity.Property(e => e.ProjectKey)
                .IsRequired()
                .HasMaxLength(32);

            entity.Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.LastSync)
                .IsRequired(false);

            entity.ToTable("JiraSyncConfigs");
        });
    }

}