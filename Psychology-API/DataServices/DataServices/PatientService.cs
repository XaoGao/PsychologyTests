using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Repositories.Repositories;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class PatientService : PatientRepository, IPatientService
    {
        private readonly ICache<Patient> _cache;
        private readonly DataContext _context;

        public PatientService(DataContext context, ICache<Patient> cache) : base(context)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<Anamnesis> CreateAnamnesisAsync(int doctorId, int patientId, Anamnesis anamnesis)
        {
            return await base.CreateAnamnesisRepositoryAsync(doctorId, patientId, anamnesis);
        }

        public async Task<IEnumerable<Anamnesis>> GetAnamnesesAsync(int patientId)
        {
            return await base.GetAnamnesesRepositoryAsync(patientId);
        }

        public async Task<Patient> GetPatientAsync(int doctorId, int patientId)
        {
            AddGetSetEvents();
            var patient = await base.GetPatientRepositoryAsync(doctorId, patientId);
            RemoveGetSetEvents();
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync(int doctorId)
        {
            return await base.GetPatientsRepositoryAsync(doctorId);
        }

        public async Task<IEnumerable<Patient>> GetPatientsForRegistryAsync()
        {
            return await base.GetPatientsForRegistryRepositoryAsync();
        }

        public async Task<Patient> GetPatientWithoutCacheAsync(int doctorId, int patientId)
        {
            base.RemoveItemInCashe += _cache.Remove;
            var patient = await base.GetPatientWithoutCacheRepositoryAsync(doctorId, patientId);
            base.RemoveItemInCashe -= _cache.Remove;
            return patient;
        }

        public void MovePatinetToArchive(Patient patient)
        {
            base.MovePatinetToArchiveRepository(patient);
        }
        /// <summary>
        /// Добавление событии на получения и внесения в кеш.
        /// </summary>
        private void AddGetSetEvents()
        {
            GetFromCashe += _cache.Get;
            SetInCashe += _cache.Set;
        }
        /// <summary>
        /// Убрать событии по получению и внесению в кеш.
        /// </summary>
        private void RemoveGetSetEvents()
        {
            GetFromCashe -= _cache.Get;
            SetInCashe -= _cache.Set;
        }
    }
}