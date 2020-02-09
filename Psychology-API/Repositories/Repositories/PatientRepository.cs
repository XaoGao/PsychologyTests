using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        private readonly DataContext _context;
        private const string suffix = "-Patient";

        public event Action<string, string, Patient> SetInCashe;
        public event Func<string, string, Patient> GetFromCashe;
        public event Action<string, string> RemoveItemInCashe;

        public PatientRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Patient> GetPatientRepositoryAsync(int doctorId, int patientId)
        {
            Patient patient = GetFromCashe(patientId.ToString(), suffix);

            if(patient == null)
            {
                patient = await GetPatientFromContext(doctorId, patientId);

                if (patient != null)
                    SetInCashe(patientId.ToString(), suffix, patient);
                else
                    patient = new Patient();
            }

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsRepositoryAsync(int doctorId)
        {
            var patients = await _context.Patients
                .Include(p => p.Doctor)
                .Include(p => p.Anamneses)
                .Where(p => p.DoctorId == doctorId && p.IsDelete != true)
                .ToListAsync();

            return patients;
        }

        public void MovePatinetToArchiveRepository(Patient patient)
        {
            patient.IsDelete = true;
        }
        public override void Add<T>(T entity) where T : class
        {
            base.Add(entity);
            Patient patient = entity as Patient;
            
            if(patient.Id != 0)
            {
                SetInCashe(patient.Id.ToString(), suffix, patient);
            }
        }

        public async Task<IEnumerable<Anamnesis>> GetAnamnesesRepositoryAsync(int patientId)
        {
            var anamneses = await _context.Anamneses
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();

            return anamneses.OrderByDescending(a => a.ConclusionTime);
        }

        public async Task<Patient> GetPatientWithoutCacheRepositoryAsync(int doctorId, int patientId)
        {
            RemoveItemInCashe(patientId.ToString(), suffix);
            var patient = await GetPatientFromContext(doctorId, patientId);

            return patient;
        }
        public async Task<Anamnesis> CreateAnamnesisRepositoryAsync(int doctorId, int patientId, Anamnesis anamnesis)
        {
            var patient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == patientId);

            var anamnesisIsLast = await _context.Anamneses.SingleOrDefaultAsync(a => a.IsLast == true && a.PatientId == patientId);

            if(anamnesisIsLast != null)
                anamnesisIsLast.IsLast = false;

            patient.Anamneses.Add(anamnesis);

            await _context.Anamneses.AddAsync(anamnesis);

            await _context.SaveChangesAsync();

            return anamnesis;
        }

        public async Task<IEnumerable<Patient>> GetPatientsForRegistryRepositoryAsync()
        {
            var patients = await _context.Patients.Where(p => p.IsDelete != true).ToListAsync();

            return patients;
        }
        private async Task<Patient> GetPatientFromContext(int doctorId, int patientId)
        {
            var patient = await _context.Patients
                    .Include(p => p.Doctor)
                    .Include(p => p.Anamneses)
                    .Include(p => p.Documents)
                    .Include(p => p.Documents)
                        .ThenInclude(d => d.DocumenType)
                    .SingleOrDefaultAsync(p =>/*p.DoctorId == doctorId &&*/ p.Id == patientId);

            return patient;
        }
    }
}