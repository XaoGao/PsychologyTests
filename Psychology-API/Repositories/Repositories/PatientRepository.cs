using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        private readonly DataContext _context;
        private readonly IMemoryCache _cache;
        private const int CAHSE_TIME_LIFE_IN_MINUT = 15;
        public PatientRepository(DataContext context, IMemoryCache cache) : base(context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<Patient> GetPatientAsync(int doctorId, int patientId)
        {
            Patient patient = null;
            if(!_cache.TryGetValue(patientId, out patient))
            {
                patient = await _context.Patients
                    .Include(p => p.Doctor)
                    .Include(p => p.Anamneses)
                    .SingleOrDefaultAsync(p => p.DoctorId == doctorId && p.Id == patientId);
                
                if(patient != null)
                    _cache.Set(patientId, patient, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CAHSE_TIME_LIFE_IN_MINUT)));
            }

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync(int doctorId)
        {
            var patients = await _context.Patients
                .Include(p => p.Doctor)
                .Include(p => p.Anamneses)
                .Where(p => p.DoctorId == doctorId)
                .ToListAsync();

            return patients;
        }

        public void MovePatinetToArchive(Patient patient)
        {
            patient.IsDelete = true;
        }
        public override void Add<T>(T entity) where T : class
        {
            base.Add(entity);
            Patient patient = entity as Patient;
            _cache.Set(patient.Id, patient, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CAHSE_TIME_LIFE_IN_MINUT)));
        }
    }
}