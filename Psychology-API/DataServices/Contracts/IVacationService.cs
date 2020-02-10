using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис для работы с графиком отпусков.
    /// </summary>
    public interface IVacationService : IBaseService
    {
        /// <summary>
        /// Получить список отпусков по всем врачам.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Vacation>> GetVacationsAsync();
        /// <summary>
        /// Список отпусков конеретного врача.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns></returns>
        Task<IEnumerable<Vacation>> GetVacationsForDoctorAsync(int doctorId);
    }
}