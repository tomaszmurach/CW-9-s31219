using APBD_CW9.Models;
using APBD_CW9.Tests;
using Xunit;

namespace APBD_CW9.UnitTests;

public class PatientGetTests
{
    [Fact]
    public void ShouldReturnPatientWithSortedPrescriptionsAndMedicaments()
    {
        var patients = TestDatabase.GetPatients();
        var prescriptions = TestDatabase.GetPrescriptions();
        var prescriptionMeds = TestDatabase.GetPrescription_Medicaments();
        var doctors = TestDatabase.GetDoctors();
        var medicaments = TestDatabase.GetMedicaments();

        var result = patients
            .Where(p => p.IdPatient == 1)
            .Select(p => new
            {
                p.IdPatient,
                p.FirstName,
                p.LastName,
                p.DateOfBirth,
                Prescriptions = prescriptions
                    .Where(r => r.IdPatient == p.IdPatient)
                    .OrderBy(r => r.DueDate)
                    .Select(r => new
                    {
                        r.IdPrescription,
                        r.Date,
                        r.DueDate,
                        Doctor = doctors.First(d => d.IdDoctor == r.IdDoctor),
                        Medicaments = prescriptionMeds
                            .Where(pm => pm.IdPrescription == r.IdPrescription)
                            .Select(pm => new
                            {
                                pm.IdMedicament,
                                pm.Dose,
                                pm.Details,
                                Name = medicaments.First(m => m.IdMedicament == pm.IdMedicament).Name
                            }).ToList()
                    }).ToList()
            })
            .FirstOrDefault();

        Assert.NotNull(result);
        Assert.Equal("Anna", result!.FirstName);
        Assert.Single(result.Prescriptions);
        Assert.Equal("Jan", result.Prescriptions[0].Doctor.FirstName);
        Assert.Single(result.Prescriptions[0].Medicaments);
        Assert.Equal("Apap", result.Prescriptions[0].Medicaments[0].Name);
        Assert.True(result.Prescriptions[0].DueDate >= result.Prescriptions[0].Date);
    }
}
