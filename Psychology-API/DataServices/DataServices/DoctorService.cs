using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.Cache;
using Psychology_API.Settings.Doctors;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class DoctorService : BaseService, IDoctorService
    {
        private readonly ICache<Doctor> _cache;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IReceptionRepository _receptionRepository;
        private const string suffix = "-Doctor";
        public DoctorService(DataContext context,
                            ICache<Doctor> cache,
                            IDoctorRepository doctorRepository,
                            IReceptionRepository receptionRepository) : base(context)
        {
            _cache = cache;
            _doctorRepository = doctorRepository;
            _receptionRepository = receptionRepository;
        }
        public async Task<Doctor> GetDoctorAsync(int doctorId)
        {
            AddGetSetEvents();
            var doctor = await _doctorRepository.GetDoctorRepositoryAsync(doctorId); 
            RemoveGetSetEvents();

            return doctor;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAsync(DoctorsType doctorsType)
        {
            return await _doctorRepository.GetDoctorsRepositoryAsync(doctorsType);
        }
        public async Task<Doctor> GetDoctorWithoutCacheAsync(int doctorId)
        {
            _doctorRepository.RemoveItemInCashe += _cache.Remove;
            var doctor = await _doctorRepository.GetDoctorWithoutCacheRepositoryAsync(doctorId);
            _doctorRepository.RemoveItemInCashe -= _cache.Remove;
            return doctor;
        }
        public async Task<IEnumerable<Reception>> GetReceptionsForDoctorsAsync(int doctorId)
        {
            return await _receptionRepository.GetReseptionsRepositoryAsync(doctorId);
        }
        /// <summary>
        /// Добавление событии на получения и внесения в кеш.
        /// </summary>
        private void AddGetSetEvents()
        {
            _doctorRepository.GetFromCashe += _cache.Get;
            _doctorRepository.SetInCashe += _cache.Set;
        }
        /// <summary>
        /// Убрать событии по получению и внесению в кеш.
        /// </summary>
        private void RemoveGetSetEvents()
        {
            _doctorRepository.GetFromCashe -= _cache.Get;
            _doctorRepository.SetInCashe -= _cache.Set;
        }
    }
}