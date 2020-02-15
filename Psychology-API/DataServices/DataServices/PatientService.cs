using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.Cache;
using Psychology_API.Settings.Patients;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class PatientService : BaseService, IPatientService
    {
        private readonly ICache<Patient> _cache;
        private readonly DataContext _context;
        private readonly IPatientRepository _patientRepository;

        public PatientService(DataContext context,
                              ICache<Patient> cache,
                              IPatientRepository patientRepository) : base(context)
        {
            _patientRepository = patientRepository;
            _context = context;
            _cache = cache;

            _patientRepository.GetFromCashe += _cache.Get;
            _patientRepository.SetInCashe += _cache.Set;
            _patientRepository.RemoveItemInCashe += _cache.Remove;
        }
        public async Task<Patient> GetPatientAsync(int doctorId, int patientId)
        {
            return await _patientRepository.GetPatientRepositoryAsync(doctorId, patientId);
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync(int doctorId, PatientsType patientsType)
        {
            return await _patientRepository.GetPatientsRepositoryAsync(doctorId, patientsType);
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync(PatientsType patientsType)
        {
            return await _patientRepository.GetPatientsRepositoryAsync(patientsType);
        }

        public async Task<Patient> GetPatientWithoutCacheAsync(int doctorId, int patientId)
        {
           
            return await _patientRepository.GetPatientWithoutCacheRepositoryAsync(doctorId, patientId);
        }

        public void MovePatinetToArchive(Patient patient)
        {
            _patientRepository.MovePatinetToArchiveRepository(patient);
        }
    }
}