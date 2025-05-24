using APBD_CW9.Data;
using APBD_CW9.DTOs;
using APBD_CW9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_CW9.Services;

public interface IDbService
{
    Task AddPrescriptionAsync(PrescriptionCreateDTO dto);
    Task<PatientGetDTO> GetPatientDetailsByIdAsync(int id);

}

public class DbService(AppDbContext data) : IDbService
{
    public async Task AddPrescriptionAsync(PrescriptionCreateDTO dto)
    {
        if (dto.Medicaments.Count > 10)
            throw new ArgumentException("Recepta może obejmować maksymalnie 10 leków.");

        if (dto.DueDate < dto.Date)
            throw new ArgumentException("DueDate nie może być wcześniejszy niż Date.");
        
        var doctor = await data.Doctors.FindAsync(dto.Doctor.IdDoctor);
        if (doctor == null)
            throw new ArgumentException("Lekarz o podanym ID nie istnieje.");
        
        Patient patient = null!;

        int maxPatientId = await data.Patients.AnyAsync() 
            ? await data.Patients.MaxAsync(p => p.IdPatient) 
            : 0;

        bool shouldCreateNew = dto.Patient.IdPatient == 0 || dto.Patient.IdPatient > maxPatientId;

        if (!shouldCreateNew)
        {
            patient = await data.Patients.FindAsync(dto.Patient.IdPatient);

            if (patient != null)
            {
                if (patient.FirstName != dto.Patient.FirstName ||
                    patient.LastName != dto.Patient.LastName ||
                    patient.DateOfBirth != dto.Patient.DateOfBirth)
                {
                    throw new ArgumentException("Dane pacjenta nie zgadzają się z ID.");
                }
            }
            else
            {
                shouldCreateNew = true;
            }
        }

        if (shouldCreateNew)
        {
            patient = new Patient
            {
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                DateOfBirth = dto.Patient.DateOfBirth
            };
            data.Patients.Add(patient);
            await data.SaveChangesAsync();
        }

        
        var prescription = new Prescription
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdDoctor = doctor.IdDoctor,
            IdPatient = patient.IdPatient,
            Prescription_Medicaments = new List<Prescription_Medicament>()
        };

        foreach (var m in dto.Medicaments)
        {
            var medicament = await data.Medicaments.FindAsync(m.IdMedicament);
            if (medicament == null)
                throw new ArgumentException($"Lek o ID {m.IdMedicament} nie istnieje.");

            prescription.Prescription_Medicaments.Add(new Prescription_Medicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            });
        }

        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();
    }
    
    
    
    
    public async Task<PatientGetDTO> GetPatientDetailsByIdAsync(int id)
    {
        var patient = await data.Patients
            .Where(p => p.IdPatient == id)
            .FirstOrDefaultAsync();

        if (patient is null)
        {
            throw new ArgumentException($"Pacjent o ID {id} nie istnieje.");
        }

        var prescriptions = await data.Prescriptions
            .Where(pr => pr.IdPatient == id)
            .Include(pr => pr.Prescription_Medicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(pr => pr.Doctor)
            .OrderBy(pr => pr.DueDate)
            .ToListAsync();

        return new PatientGetDTO
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Prescriptions = prescriptions.Select(p => new PrescriptionDTO
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Medicaments = p.Prescription_Medicaments.Select(pm => new MedicamentDTO
                {
                    IdMedicament = pm.IdMedicament,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Details
                }).ToList(),
                Doctor = new DoctorDTO
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName
                }
            }).ToList()
        };
    }

    
}
