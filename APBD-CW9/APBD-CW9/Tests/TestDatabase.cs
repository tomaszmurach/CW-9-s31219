using APBD_CW9.Models;

namespace APBD_CW9.Tests;

public static class TestDatabase
{
    public static List<Doctor> GetDoctors() => new List<Doctor>
    {
        new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Nowak", Email = "jan@example.com" }
    };

    public static List<Medicament> GetMedicaments() => new List<Medicament>
    {
        new Medicament { IdMedicament = 1, Name = "Apap", Description = "Przeciwbólowy", Type = "Tabletka" }
    };

    public static List<Patient> GetPatients() => new List<Patient>
    {
        new Patient { IdPatient = 1, FirstName = "Anna", LastName = "Kowalska", DateOfBirth = new DateTime(1990, 1, 1) }
    };

    public static List<Prescription> GetPrescriptions() => new List<Prescription>
    {
        new Prescription
        {
            IdPrescription = 1,
            IdPatient = 1,
            IdDoctor = 1,
            Date = new DateTime(2024, 05, 20),
            DueDate = new DateTime(2024, 05, 30)
        }
    };

    public static List<Prescription_Medicament> GetPrescription_Medicaments() => new List<Prescription_Medicament>
    {
        new Prescription_Medicament
        {
            IdPrescription = 1,
            IdMedicament = 1,
            Dose = 2,
            Details = "Stosować rano"
        }
    };
}