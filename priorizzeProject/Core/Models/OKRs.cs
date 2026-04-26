using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Models;
[Table("OKRs")]
public class OKRs
{   
    [Key]
    public int Id { get; set; }
    
    [MaxLength(100)]
    public String Title { get; set; }
    
    [MaxLength(1000)]
    public String? Description { get; set; }
    
    [Required]
    [ForeignKey(nameof(Cycle))]
    public int CycleId { get; set; }

    public int DirectorId { get; set; }
    
    public int? ManagerId { get; set; }//se o manager for null significa que a okr eh global
    
    public OkrStatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public DateTime FinishedAt { get; set; }

    public OKRs(string title, string? description, int cycleId, int directorId, int? managerId)
    {
        Title = title;
        Description = description;
        CycleId = cycleId;
        DirectorId = directorId;
        ManagerId = managerId;
        
        Status = OkrStatusEnum.Criado; 
        CreatedAt = DateTime.UtcNow;
    }

    public OKRs()
    {
        
    }


    public void IniciarOkr()
    {
        if (Status == OkrStatusEnum.Criado)
            Status = OkrStatusEnum.Ativo;
    }

    public void FinalizarOkr()
    {
        Status = OkrStatusEnum.Concluido;
    }

    public void CancelarOkr()
    {
        Status = OkrStatusEnum.Cancelado;
    }
}