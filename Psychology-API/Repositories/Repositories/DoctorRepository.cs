using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class DoctorRepository : BaseRepository, IDoctorRepository
    {
        private readonly DataContext _context;
        private readonly ICache<Doctor> _cache;
        public DoctorRepository(DataContext context, ICache<Doctor> cache) : base(context)
        {
            _cache = cache;
            _context = context;
        }
        public async Task<Doctor> GetDoctorAsync(int doctorId)
        {
            Doctor doctor = null;

            string key = doctorId + "-Doctor";

            if(!_cache.Get(key, out doctor))
            {
                doctor = await GetDoctorFromContext(doctorId);

                if (doctor != null)
                    _cache.Set(key, doctor);
            }

            return doctor;
        }

        public async Task<Doctor> GetDoctorWithoutCacheAsync(int doctorId)
        {
            var doctor = await GetDoctorFromContext(doctorId);

            return doctor;
        }
        private async Task<Doctor> GetDoctorFromContext(int doctorId)
        {
            var doctor = await _context.Doctors
                    .Include(d => d.Phone)
                    .Include(d => d.Position)
                    .Include(d => d.Department)
                    .SingleOrDefaultAsync(d => d.Id == doctorId);

            return doctor;
        }
    }
}