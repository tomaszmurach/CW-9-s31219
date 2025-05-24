namespace APBD_CW9.DTOs;

public class PatientGetDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }

    public List<PrescriptionDTO> Prescriptions { get; set; } = new();
}

public class PrescriptionDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public List<MedicamentDTO> Medicaments { get; set; } = new();
    public DoctorDTO Doctor { get; set; } = null!;
}

public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = null!;
    public int? Dose { get; set; }
    public string Description { get; set; } = null!;
}

public class DoctorDTO
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
}
