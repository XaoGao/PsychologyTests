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
        public async Task<Doctor> GetDoctorAsync(int doctorId)
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