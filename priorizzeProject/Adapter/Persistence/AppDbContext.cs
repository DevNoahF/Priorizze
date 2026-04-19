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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureCycleEntity(modelBuilder);

        ConfigureMetricsHistoryEntity(modelBuilder);

        ConfigureJiraProjectsEntity(modelBuilder);
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

            entity.Property(e => e.LasSync)
                .IsRequired(false);

            entity.ToTable("JiraProjects");
        });
    }
}