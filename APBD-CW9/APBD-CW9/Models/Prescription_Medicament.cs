using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW9.Models;


[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdPrescription), nameof(IdMedicament))]
public class Prescription_Medicament
{
    [Column("IdMedicament")]
    public int IdMedicament { get; set; }
    
    [Column("IdPrescription")]
    public int IdPrescription { get; set; }
    
    [Column("Dose")]
    public int? Dose { get; set; }

    [Column("Details")]
    [MaxLength(100)]
    public string Details { get; set; } = null!;
    
    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament Medicament { get; set; } = null!;
    
    [ForeignKey(nameof(IdPrescription))]
    public virtual Prescription Prescription { get; set; } = null!;
}