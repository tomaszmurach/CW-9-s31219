using System.ComponentModel.DataAnnotations;

namespace APBD_CW9.DTOs;

public class PrescriptionCreateDTO
{
    [Required]
    public Models.Patient Patient { get; set; } = null!;

    [Required]
    public DoctorRefDTO Doctor { get; set; } = null!;

    [Required]
    [MinLength(1)]
    [MaxLength(10)]
    public List<PrescriptionMedicamentEntry> Medicaments { get; set; } = new();

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime DueDate { get; set; }
}

public class PrescriptionMedicamentEntry
{
    [Required]
    public int IdMedicament { get; set; }

    public int? Dose { get; set; }

    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = null!;
}

public class DoctorRefDTO
{
    [Required]
    public int IdDoctor { get; set; }
}