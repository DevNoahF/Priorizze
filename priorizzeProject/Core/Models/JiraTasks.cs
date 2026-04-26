using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace priorizzeProject.Core.Models;

[Table("Jira_Tasks")]
[Index(nameof(ExternalKey), IsUnique = true)]
public class JiraTasks
{
    
    /// <summary>
    /// Entidade que representa uma Tarefa/Key Result sincronizada com o Jira.
    /// 
    /// BASEADA NA API OFICIAL DO JIRA CLOUD (REST API v3):
    /// API: Search for issues using JQL
    /// Endpoint: GET /rest/api/3/search?jql={sua_query}
    /// (Também suporta POST em /rest/api/3/search para JQLs muito grandes)
    /// 
    /// Mapeamento de Retorno (JSON):
    /// - A propriedade 'key' do root do JSON mapeia para 'ExternalKey' (ex: PROJ-123).
    /// - A maioria dos dados vem do objeto 'fields':
    ///   * fields.summary -> Summary
    ///   * fields.issuetype.name -> IssueType
    ///   * fields.status.name -> StatusName
    ///   * fields.priority.name -> PriorityName
    ///   * fields.assignee.accountId -> AssigneeAccountId
    ///   * fields.created, fields.updated, fields.resolutiondate -> Campos de Data (DateTimeOffset)
    ///   * fields.customfield_XXXXX -> StoryPoints (O ID exato varia por instância do Jira)
    /// </summary>
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    [Required]
    [MaxLength(50)]
    public string ExternalKey { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string Summary { get; set; } = string.Empty;
    
    [Required]
    public int ProjectId { get; set; }

    [Required]
    public int SquadId { get; set; }

    public int? KpiId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string IssueType { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string StatusName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string PriorityName { get; set; } = string.Empty;

    // Mapeia para fields.customfield_XXXXX (O ID exato você verá no JSON da sua instância do Jira)
    public double? StoryPoints { get; set; }
    
    [MaxLength(128)]
    public string? AssigneeAccountId { get; set; }

    // Alterado para DateTimeOffset para respeitar o ISO 8601 do Jira
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? ResolvedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }   
    
    public JiraTasks(string externalKey, string summary, int projectId, int squadId, 
        string issueType, string statusName, string priorityName, 
        string? assigneeAccountId, DateTimeOffset createdAt, 
        double? storyPoints = null, int? kpiId = null)
    {
        ExternalKey = externalKey;
        Summary = summary;
        ProjectId = projectId;
        SquadId = squadId;
        IssueType = issueType;
        StatusName = statusName;
        PriorityName = priorityName;
        AssigneeAccountId = assigneeAccountId;
        CreatedAt = createdAt;
        UpdatedAt = DateTimeOffset.UtcNow; 
        StoryPoints = storyPoints;
        KpiId = kpiId;
    }

    public JiraTasks()
    {
        
    }

    public void SyncWithJiraApi(string summary, string statusName, string priorityName, 
        string? assigneeAccountId, double? storyPoints, 
        DateTimeOffset? resolvedAt, DateTimeOffset jiraUpdatedAt)
    {
        Summary = summary;
        StatusName = statusName;
        PriorityName = priorityName;
        AssigneeAccountId = assigneeAccountId;
        StoryPoints = storyPoints;
        ResolvedAt = resolvedAt;
        UpdatedAt = jiraUpdatedAt; 
    }
    
}