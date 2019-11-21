using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Интерфейс доктора.
    /// </summary>
    public interface IDoctorRepository : IBaseRepository
    {
        /// <summary>
        /// Получить все актуальных пациентов конкретного доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Пациенты, которые записаны на доктора. </returns>
        Task<IEnumerable<Patient>> GetPatientsAsync(int doctorId);
        /// <summary>
        /// Получить конкретного пациента доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Пациент. </returns>
        Task<Patient> GetPatientAsync(int doctorId, int patientId);
        /// <summary>
        /// Перевести пациента в архив.
        /// </summary>
        /// <param name="patient"> Пациент. </param>
        /// <returns></returns>
        void MovePatinetToArchive(Patient patient);
    }
}