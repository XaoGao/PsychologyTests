using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        private readonly DataContext _context;
        private readonly ICache<Patient> _cache;

        public PatientRepository(DataContext context, ICache<Patient> cache) : base(context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<Patient> GetPatientAsync(int doctorId, int patientId)
        {
            Patient patient = null;

            string key = doctorId + "-Patient";

            if(!_cache.Get(key, out patient))
            {
                patient = await _context.Patients
                    .Include(p => p.Doctor)
                    .Include(p => p.Anamneses)
                    .SingleOrDefaultAsync(p => p.DoctorId == doctorId && p.Id == patientId);

                if (patient != null)
                    _cache.Set(key, patient);
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
            _cache.Set(patient.Id + "-Patient", patient);
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