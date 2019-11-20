using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Интерфейс доктора.
    /// </summary>
    public interface IDoctorRepository
    {
        /// <summary>
        /// Получить все пациентов конкретного доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Пациенты, которые записаны на доктора. </returns>
        Task<IEnumerable<Patient>> GetPatientsAsync(int doctorId);
    }
}