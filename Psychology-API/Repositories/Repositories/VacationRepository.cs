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
    public class VacationRepository : BaseRepository, IVacationRepository
    {
        private readonly DataContext _context;
        public VacationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vacation>> GetVacations()
        {
            var vacations = await _context.Vacations
                .Include(v => v.Doctor)
                .Where(v => v.EndVacation >= DateTime.Now)
                .ToListAsync();

            return vacations;
        }

        public async Task<IEnumerable<Vacation>> GetVacationsForDoctor(int doctorId)
        {
            var vacations = await _context.Vacations
                .Include(v => v.Doctor)
                .Where(v => v.DoctorId == doctorId)
                .OrderByDescending(v => v.StartVacation)
                .ToListAsync();

            return vacations;
        }
    }
}