using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.Cache;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class DoctorRepository : BaseRepository, IDoctorRepository
    {
        private readonly DataContext _context;
        private const string suffix = "-Doctor";

        /// <summary>
        /// Событие на добавления объекта из кеш.
        /// </summary>
        public event Action<string, string, Doctor> SetInCashe;
        /// <summary>
        /// Событие на получение объекта из кеш.
        /// </summary>
        public event Func<string, string, Doctor> GetFromCashe;
        /// <summary>
        /// Событие на удаление объекта из кеш.
        /// </summary>
        public event Action<string, string> RemoveItemInCashe;

        public DoctorRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Doctor> GetDoctorRepositoryAsync(int doctorId)
        {
            Doctor doctor = GetFromCashe(doctorId.ToString(), suffix);
            if( doctor == null) 
            {
                doctor = await GetDoctorFromContext(doctorId);

                if (doctor != null)
                    SetInCashe(doctorId.ToString(), suffix, doctor);
            }

            return doctor;
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsRepositoryAsync()
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Name.Equals(RolesSettings.Doctor));

            if(role == null)
                throw new Exception("Возникла ошибка. Обратитесь к администратору для утрочнее ролей пользователей системы");

            var doctors = await _context.Doctors
                .Where(d => d.RoleId == role.Id && d.IsDeleted == false)
                .Include(d => d.Patients)
                .ToListAsync();

            return doctors;
        }

        public async Task<Doctor> GetDoctorWithoutCacheRepositoryAsync(int doctorId)
        {
            RemoveItemInCashe(doctorId.ToString(), suffix);
            var doctor = await GetDoctorFromContext(doctorId);

            return doctor;
        }

        public async Task<IEnumerable<Reception>> GetReceptionsForDoctorsRepositoryAsync(int doctorId)
        {
            var receptions = await _context.Receptions
                .Include(r => r.Patient)
                .Where(r => r.DoctorId == doctorId)
                .ToListAsync();

            return receptions;
        }

        private async Task<Doctor> GetDoctorFromContext(int doctorId)
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