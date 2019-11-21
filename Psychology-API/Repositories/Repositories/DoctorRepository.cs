using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class DoctorRepository : BaseRepository, IDoctorRepository
    {
        private readonly DataContext _context;
        public DoctorRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Patient> GetPatientAsync(int doctorId, int patientId)
        {
            var patient = await _context.Patients.SingleOrDefaultAsync(p => p.DoctorId == doctorId && p.Id == patientId);

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync(int doctorId)
        {
            var patients = await _context.Patients.Where(p => p.DoctorId == doctorId).ToListAsync();
            return patients;
        }

        public void MovePatinetToArchive(Patient patient)
        {
            patient.IsDelete = true;
        }
    }
}