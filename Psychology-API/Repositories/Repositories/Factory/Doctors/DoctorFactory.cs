using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Settings;
using Psychology_API.Settings.Doctors;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories.Factory.Doctors
{ 
    /// <summary>
    /// Класс для получении пользователей сисетмы.
    /// </summary>
    public class DoctorFactory
    {
        private readonly DataContext _context;
        /// <summary>
        /// Создать новый экземпляр класса.
        /// </summary>
        /// <param name="context"></param>
        public DoctorFactory(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод который вернет список докторов в зависимости от типа.
        /// </summary>
        /// <param name="doctorsType"> Условие, которые указывает каких докторов вернуть. </param>
        /// <returns> Список врачей. </returns>
        public async Task<IEnumerable<Doctor>> GetDoctors(DoctorsType doctorsType)
        {
            switch (doctorsType)
            {
                case DoctorsType.AllDoctors:
                    return await GetAllDoctorsAsync();
                case DoctorsType.EnableDoctors:
                    return await GetEnableDoctorsAsync();
                case DoctorsType.DoctorsWithRoleDoctor:
                    return await GetDoctorsWithRoleDoctorAsync();
                default:
                    return null;
            }
        }
        /// <summary>
        /// Возвращает всех пользователей системы.
        /// </summary>
        /// <returns> Список врачей в системе. </returns>
        private async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }
        /// <summary>
        /// Возвращает только активных пользователей сисетмы.
        /// </summary>
        /// <returns> Список активных врачей. </returns>
        private async Task<IEnumerable<Doctor>> GetEnableDoctorsAsync()
        {
            return await _context.Doctors.Where(d => d.IsLock == false).ToListAsync();
        }
        /// <summary>
        /// Возвращает только активных врачей с ролью Доктор.
        /// </summary>
        /// <returns> Список активных врачей с ролью доктор. </returns>
        private async Task<IEnumerable<Doctor>> GetDoctorsWithRoleDoctorAsync()
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Name.Equals(RolesSettings.Doctor));

            if(role == null)
                throw new Exception("Возникла ошибка. Обратитесь к администратору для утрочнее ролей пользователей системы");

            var doctors = await _context.Doctors
                .Where(d => d.IsLock == false && d.Role.Id == role.Id)
                .Include(d => d.Patients)
                .ToListAsync();

            foreach (var item in doctors)
            {
                item.Patients = item.Patients.Where(p => p.IsLock == false).ToList();
            }
            return doctors;
        }
    }
}