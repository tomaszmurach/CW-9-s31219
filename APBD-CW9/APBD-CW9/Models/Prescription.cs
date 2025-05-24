using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_CW9.Models;


[Table("Prescription")]
public class Prescription
{
    [Key]
    [Column("IdPrescription")]
    public int IdPrescription { get; set; }
    
    
    [Column("Date", TypeName = "date")]
    public DateTime Date { get; set; }
    
    [Column("DueDate", TypeName = "date")]
    public DateTime DueDate { get; set; }
    
    [Column("IdPatient")]
    public int IdPatient { get; set; }
    
    [Column("IdDoctor")]
    public int IdDoctor { get; set; }
    
    [ForeignKey(nameof(IdPatient))]
    public virtual Patient Patient { get; set; } = null!;
    
    
    [ForeignKey(nameof(IdDoctor))]
    public virtual Doctor Doctor { get; set; } = null!;
    
    public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }

    
    
}