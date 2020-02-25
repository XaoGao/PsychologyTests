using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Repositories.Repositories.Factory.Patients;
using Psychology_API.Settings.Patients;
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

        public async Task<IEnumerable<Patient>> GetPatientsRepositoryAsync(int doctorId, PatientsType patientsType)
        {
            PatientFactory patientFactory = new PatientFactory(_context);

            var patients = await patientFactory.GetPatientAsync(doctorId, patientsType);

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
        public async Task<Patient> GetPatientWithoutCacheRepositoryAsync(int doctorId, int patientId)
        {
            RemoveItemInCashe(patientId.ToString(), suffix);
            var patient = await GetPatientFromContext(doctorId, patientId);

            return patient;
        }
        public async Task<IEnumerable<Patient>> GetPatientsRepositoryAsync(PatientsType patientsType)
        {
            PatientFactory patientFactory = new PatientFactory(_context);

            var patients = await patientFactory.GetPatientAsync(patientsType);

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
                    .SingleOrDefaultAsync(p => p.Id == patientId);

            return patient;
        }

        public async Task<bool> PatientIsExistRepositoryAsync(string patientNumberCard)
        {
            if (await _context.Patients.AnyAsync(p => p.PersonalCardNumber.Equals(patientNumberCard)))
                return true;

            return false;
        }
    }
}