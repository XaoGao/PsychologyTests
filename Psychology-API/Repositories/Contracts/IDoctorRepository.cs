using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Settings.Doctors;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Интерфейса репозитория доктора.
    /// </summary>
    public interface IDoctorRepository : IBaseRepository, ICasheable<Doctor>
    {
        /// <summary>
        /// Получить данные конкретного доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns></returns>
        Task<Doctor> GetDoctorRepositoryAsync(int doctorId);
        /// <summary>
        /// Получить данные конкретного доктора без записи данных в кеш.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns></returns>
        Task<Doctor> GetDoctorWithoutCacheRepositoryAsync(int doctorId);
        /// <summary>
        /// Получить список лечащих врачей.
        /// </summary>
        /// <returns> Список рабртающих врачей. </returns>
        Task<IEnumerable<Doctor>> GetDoctorsRepositoryAsync(DoctorsType doctorType);
        /// <summary>
        /// Получить приемы пациентов у конкретного врача в течении текущей недели.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Список приемов у врача. </returns>
        // Task<IEnumerable<Reception>> GetReceptionsForDoctorsRepositoryAsync(int doctorId);
    }
}