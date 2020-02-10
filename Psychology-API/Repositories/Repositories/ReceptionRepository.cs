using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;
using Psychology_API.Helpers;

namespace Psychology_API.Repositories.Repositories
{
    public class ReceptionRepository : BaseRepository, IReceptionRepository
    {
        private readonly DataContext _context;

        public ReceptionRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckReceptionTimeRepositoryAsync(int doctorId, DateTime timeReception)
        {
            var receptions = await GetReseptionsRepositoryAsync(doctorId);

            if(receptions.Any(r => r.DateTimeReception == timeReception))
                return false;

            var vacations = await _context.Vacations.Where(v => v.DoctorId == doctorId).ToListAsync();

            if(vacations.Any(v => v.StartVacation < timeReception && timeReception < v.EndVacation))
                return false;

            return true;
        }

        public async Task<IEnumerable<Reception>> GetReseptionsRepositoryAsync(int doctorId)
        {
            var receptions = await _context.Receptions
                .Where(r => r.DoctorId == doctorId)
                .ToListAsync();

            return receptions;
        }
        public async Task<IEnumerable<DateTime>> GetFreeReceptionTimeForDayRepositoryAsync(int doctorId, DateTime dateTimeReception)
        {
            var allWorkTimesOfDoctor = getAllWorkTimes(dateTimeReception);

            var receptions = await _context.Receptions
                .Where(r => r.DoctorId == doctorId && r.DateTimeReception >= DateTime.Now)
                .ToListAsync();

            List<DateTime> freeTimeOfDoctors = new List<DateTime>();
            foreach (var time in allWorkTimesOfDoctor)
            {
                if(receptions.Any(r => r.DateTimeReception == time))
                    continue;

                freeTimeOfDoctors.Add(time);
            }

            return freeTimeOfDoctors;
        }
        public async Task<IEnumerable<Reception>> GetReseptionsOfCurrentWeekRepositoryAsync(int doctorId, DateTime now)
        {
            var day = DayOfWeek.Monday;
            var dateStartWeek = now.GetDateOfStartWeek(day);
            var dateEndWeek = now.GetDateOfEndWeek(day);

            var receptions = await _context.Receptions
                .Include(r => r.Patient)
                .Where(r => r.DoctorId == doctorId && r.DateTimeReception >= dateStartWeek && r.DateTimeReception <= dateEndWeek)
                .ToListAsync();

            return receptions;
        }
        private List<DateTime> getAllWorkTimes(DateTime day)
        {
            List<DateTime> times = new List<DateTime>();
            for (int i = 8; i <= 18; i++)
            {
                DateTime time = new DateTime(day.Year, day.Month, day.Day, i, 0, 0);
                times.Add(time);
            }
            return times;
        }
    }
}