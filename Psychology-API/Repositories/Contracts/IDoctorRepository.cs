using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Интерфейса репозитория доктора.
    /// </summary>
    public interface IDoctorRepository : IBaseRepository
    {
        /// <summary>
        /// Получить данные конкретного доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns></returns>
        Task<Doctor> GetDoctorAsync(int doctorId);
        /// <summary>
        /// Получить данные конкретного доктора без записи данных в кеш.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns></returns>
        Task<Doctor> GetDoctorWithoutCacheAsync(int doctorId);
    }
}