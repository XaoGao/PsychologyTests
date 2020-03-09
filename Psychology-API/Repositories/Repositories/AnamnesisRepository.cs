using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class AnamnesisRepository : BaseRepository, IAnamnesisRepository
    {
        private readonly DataContext _context;
        public AnamnesisRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Anamnesis> CreateAnamnesisRepositoryAsync(int doctorId, int patientId, Anamnesis anamnesis)
        {
            var patient = await _context.Patients.SingleOrDefaultAsync(p => p.Id == patientId);

            var anamnesisIsLast = await _context.Anamneses.SingleOrDefaultAsync(a => a.IsLast == true && a.PatientId == patientId);

            if (anamnesisIsLast != null)
                anamnesisIsLast.IsLast = false;

            await _context.Anamneses.AddAsync(anamnesis);

            await _context.SaveChangesAsync();

            return anamnesis;
        }

        public async Task<IEnumerable<Anamnesis>> GetAnamnesesRepositoryAsync(int patientId)
        {
            var anamneses = await _context.Anamneses
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();

            return anamneses.OrderByDescending(a => a.ConclusionTime);
        }
    }
}