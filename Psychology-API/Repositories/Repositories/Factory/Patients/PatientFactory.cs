using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Settings.Patients;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories.Factory.Patients
{
    /// <summary>
    /// Класс для получении пользователей сисетмы.
    /// </summary>
    public class PatientFactory
    {
        private readonly DataContext _context;
        /// <summary>
        /// Создать новый экземпляр класса.
        /// </summary>
        /// <param name="context"></param>
        public PatientFactory(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод который вернет список пациентов в зависимости от типа.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientType"> Условие, которые указывает каких пациентов вернуть. </param>
        /// <returns></returns>
        public async Task<IEnumerable<Patient>> GetPatientAsync(int doctorId, PatientsType patientsType)
        {
            if (patientsType == PatientsType.PatientsOfDoctor)
                return await GetPatientsOfDoctors(doctorId);
            
            return null;
        }
        /// <summary>
        /// Метод который вернет список пациентов, которые закреплены за врачем.
        /// </summary>
        /// <param name="patientsType"> Условие, которые указывает каких пациентов вернуть. </param>
        /// <returns> Список пациентов. </returns>
        public async Task<IEnumerable<Patient>> GetPatientAsync(PatientsType patientsType)
        {
            switch (patientsType)
            {
                case PatientsType.AllPatients:
                    return await GetAllPatientAsync();
                case PatientsType.EnablePatients:
                    return await GetEnablePatients();
                default:
                    return null;
            }
        }
        /// <summary>
        /// Вернуть всех пациентов.
        /// </summary>
        /// <returns> Список пациентов. </returns>
        private async Task<IEnumerable<Patient>> GetAllPatientAsync()
        {
            return await _context.Patients.ToListAsync();
        }
        /// <summary>
        /// Вернуть активных пациентов.
        /// </summary>
        /// <returns> Список пациентов. </returns>
        private async Task<IEnumerable<Patient>> GetEnablePatients()
        {
            return await _context.Patients.Where(p => p.IsLock != true).ToListAsync();
        }
        /// <summary>
        /// Вернуть пациентов, которые закреплены за врачем.
        /// </summary>
        /// <param name="doctorId"> Идентификатор врача. </param>
        /// <returns> Список пациентов. </returns>
        private async Task<IEnumerable<Patient>> GetPatientsOfDoctors(int doctorId)
        {
            return await _context.Patients
                .Include(p => p.Doctor)
                .Include(p => p.Anamneses)
                .Where(p => p.DoctorId == doctorId && p.IsLock != true)
                .ToListAsync();
        }
    }
}