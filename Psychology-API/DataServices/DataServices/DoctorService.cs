using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Repositories;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class DoctorService : DoctorRepository, IDoctorService
    {
        private readonly ICache<Doctor> _cache;
        private const string suffix = "-Doctor";
        public DoctorService(DataContext context, ICache<Doctor> cache) : base(context)
        {
            _cache = cache;
        }

        public async Task<Doctor> GetDoctorAsync(int doctorId)
        {
            AddGetSetEvents();
            var doctor = await base.GetDoctorRepositoryAsync(doctorId); 
            RemoveGetSetEvents();

            return doctor;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAsync()
        {
            return await base.GetDoctorsRepositoryAsync();
        }

        public async Task<Doctor> GetDoctorWithoutCacheAsync(int doctorId)
        {
            base.RemoveItemInCashe += _cache.Remove;
            var doctor = await base.GetDoctorWithoutCacheRepositoryAsync(doctorId);
            base.RemoveItemInCashe -= _cache.Remove;
            return doctor;
        }

        public async Task<IEnumerable<Reception>> GetReceptionsForDoctorsAsync(int doctorId)
        {
            return await base.GetReceptionsForDoctorsRepositoryAsync(doctorId);
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