using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class DoctorRepository : BaseRepository, IDoctorRepository
    {
        private readonly DataContext _context;
        private readonly IMemoryCache cache;
        private const int CAHSE_TIME_LIFE_IN_MINUT = 15;
        public DoctorRepository(DataContext context, IMemoryCache cache) : base(context)
        {
            this.cache = cache;
            _context = context;
        }
        public async Task<Doctor> GetDoctorAsync(int doctorId)
        {
            Doctor doctor = null;

            if(!cache.TryGetValue(doctorId, out doctor))
            {
                doctor = await _context.Doctors
                    .Include(d => d.Phone)
                    .Include(d => d.Position)
                    .Include(d => d.Department)
                    .SingleOrDefaultAsync(d => d.Id == doctorId);

                if(doctor != null)
                    cache.Set(doctorId, doctorId, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CAHSE_TIME_LIFE_IN_MINUT)));
            }
            // var doctor = await _context.Doctors
            //         .Include(d => d.Phone)
            //         .Include(d => d.Position)
            //         .Include(d => d.Department)
            //         .SingleOrDefaultAsync(d => d.Id == doctorId);

            return doctor;
        }
    }
}