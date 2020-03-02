using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Репозитории для работы с отпусками.
    /// </summary>
    public interface IVacationRepository : IBaseRepository
    {
        /// <summary>
        /// Получить список все отпусков для всех пользователей.
        /// </summary>
        /// <returns> Список отпусков. </returns>
        Task<IEnumerable<Vacation>> GetVacationsRepositoryAsync();
        /// <summary>
        /// Получить список отпусков для конкреного врача.
        /// </summary>
        /// <param name="doctorId"> Идентификатор врача. </param>
        /// <returns> Список врачей. </returns>
        Task<IEnumerable<Vacation>> GetVacationsForDoctorRepositoryAsync(int doctorId);
    }
}