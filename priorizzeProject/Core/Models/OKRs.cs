using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using priorizzeProject.Core.Models.Enums;

namespace priorizzeProject.Core.Models;
[Table("OKRs")]
public class OKRs
{   
    [Key]
    public int Id { get; private set; }
    
    [MaxLength(100)]
    public String Title { get; private set; }
    
    [MaxLength(1000)]
    public String? Description { get; private set; }
    
    [Required]
    [ForeignKey(nameof(Cycle))]
    public int CycleId { get; private set; }
    
    public int? ManagerId { get; private set; }//se o manager for null significa que a okr eh global
    
    public OkrStatusEnum Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime FinishedAt { get; set; }
    
    public Okr()
    {
        Id = Guid.NewGuid();
    }
        
}