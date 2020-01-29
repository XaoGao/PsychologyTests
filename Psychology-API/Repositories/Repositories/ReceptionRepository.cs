using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class ReceptionRepository : BaseRepository, IReceptionRepository
    {
        private readonly DataContext _context;

        public ReceptionRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckFreeReceptionTime(int doctorId, DateTime timeReception)
        {
            var receptions = await GetReseptionsAsync(doctorId);

            if(receptions.Any(r => r.DateTimeReception == timeReception))
                return false;

            return true;
        }

        public async Task<IEnumerable<Reception>> GetReseptionsAsync(int doctorId)
        {
            var receptions = await _context.Receptions.Where(r => r.DoctorId == doctorId).ToListAsync();

            return receptions;
        }
    }
}