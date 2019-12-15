using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        private readonly DataContext _context;
        private readonly IMemoryCache _cache;
        private readonly CacheSettings _cacheSettings;
        public PatientRepository(DataContext context, IMemoryCache cache, CacheSettings cacheSettings) : base(context)
        {
            _cacheSettings = cacheSettings;
            _cache = cache;
            _context = context;
        }

        public async Task<Patient> GetPatientAsync(int doctorId, int patientId)
        {
            Patient patient = null;

            string key = doctorId + "-Patient";

            if (!_cache.TryGetValue(key, out patient))
            {
                patient = await _context.Patients
                    .Include(p => p.Doctor)
                    .Include(p => p.Anamneses)
                    .SingleOrDefaultAsync(p => p.DoctorId == doctorId && p.Id == patientId);

                if (patient != null)
                    _cache.Set(key, patient, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheSettings.TimeLifeInMinut)));
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
            _cache.Set(patient.Id + "-Patient", patient, new MemoryCacheEntryOptions()
                                                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheSettings.TimeLifeInMinut)));
        }

        public async Task<IEnumerable<Anamnesis>> GetAnamnesesAsync(int patientId)
        {
            var anamneses = await _context.Anamneses
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();

            return anamneses;
        }
    }
}