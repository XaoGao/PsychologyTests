using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис для работы с доктором.
    /// </summary>
    public interface IDoctorService : IBaseService
    {
        /// <summary>
        /// Получить из репозитория доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Доктор. </returns>
        Task<Doctor> GetDoctorAsync(int doctorId);
        /// <summary>
        /// Получить из репозитория доктора и удалисть из кеш.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Доктор. </returns>
        Task<Doctor> GetDoctorWithoutCacheAsync(int doctorId);
        /// <summary>
        /// Получить список актуальных пользователей с ролью доктор.
        /// </summary>
        /// <returns> Список врачей. </returns>
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        /// <summary>
        /// Получить список приемов у конкретного доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Список приемов у врача. </returns>
        Task<IEnumerable<Reception>> GetReceptionsForDoctorsAsync(int doctorId);
    }
}