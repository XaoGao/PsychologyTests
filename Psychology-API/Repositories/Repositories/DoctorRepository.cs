using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class DoctorRepository : BaseRepository, IDoctorRepository
    {
        private readonly DataContext _context;
        private readonly IMemoryCache _cache;
        private readonly CacheSettings _cacheSettings;
        public DoctorRepository(DataContext context, IMemoryCache cache, CacheSettings cacheSettings) : base(context)
        {
            _cacheSettings = cacheSettings;
            _cache = cache;
            _context = context;
        }
        public async Task<Doctor> GetDoctorAsync(int doctorId)
        {
            Doctor doctor = null;

            string key = doctorId + "-Doctor";

            if (!_cache.TryGetValue(key, out doctor))
            {
                doctor = await _context.Doctors
                    .Include(d => d.Phone)
                    .Include(d => d.Position)
                    .Include(d => d.Department)
                    .SingleOrDefaultAsync(d => d.Id == doctorId);

                if (doctor != null)
                    _cache.Set(key, doctorId, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheSettings.TimeLifeInMinut)));
            }

            return doctor;
        }
    }
}