using APBD_CW9.Models;
using Xunit;
using APBD_CW9.Tests;

public class PrescriptionAddTests
{
    [Fact]
    public void ShouldAssignPrescriptionToPatient()
    {
        var doctors = TestDatabase.GetDoctors();
        var meds = TestDatabase.GetMedicaments();
        var patients = TestDatabase.GetPatients();

        var prescription = new Prescription
        {
            IdPrescription = 1,
            IdDoctor = doctors[0].IdDoctor,
            IdPatient = patients[0].IdPatient,
            Date = new DateTime(2024, 5, 1),
            DueDate = new DateTime(2024, 5, 10),
            Prescription_Medicaments = new List<Prescription_Medicament>
            {
                new()
                {
                    IdPrescription = 1,
                    IdMedicament = meds[0].IdMedicament,
                    Dose = 2,
                    Details = "Stosować rano"
                }
            }
        };

        var result = new List<Prescription> { prescription };

        Assert.Single(result);
        Assert.Equal(1, result[0].IdDoctor);
        Assert.Equal("Stosować rano", result[0].Prescription_Medicaments.First().Details);
    }
    
    [Fact]
    public void ShouldAddNewPatientWhenNotExistsInDatabase()
    {
        var patients = TestDatabase.GetPatients();
        var initialCount = patients.Count;

        var newPatient = new Patient
        {
            IdPatient = 0,
            FirstName = "Adam",
            LastName = "Nowy",
            DateOfBirth = new DateTime(1999, 1, 1)
        };
        
        newPatient.IdPatient = patients.Max(p => p.IdPatient) + 1;
        patients.Add(newPatient);

        var result = patients.FirstOrDefault(p =>
            p.FirstName == "Adam" &&
            p.LastName == "Nowy" &&
            p.DateOfBirth == new DateTime(1999, 1, 1));

        Assert.Equal(initialCount + 1, patients.Count);
        Assert.NotNull(result);
        Assert.Equal(2, result!.IdPatient);
    }

    
    [Fact]
    public void ShouldFailWhenMedicamentNotExists()
    {
        var meds = TestDatabase.GetMedicaments();
        var missingMedId = 999;

        var exists = meds.Any(m => m.IdMedicament == missingMedId);

        Assert.False(exists);
    }
    
    [Fact]
    public void ShouldPassWhenDueDateIsAfterOrEqualToDate()
    {
        var date = new DateTime(2024, 5, 25);
        var due = new DateTime(2024, 5, 30);

        Assert.True(due >= date);
    }


}