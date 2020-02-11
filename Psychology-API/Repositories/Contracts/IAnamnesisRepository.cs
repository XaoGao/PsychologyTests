using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Репозитории для работы с заключениями.
    /// </summary>
    public interface IAnamnesisRepository : IBaseRepository
    {
        /// <summary>
        /// Получить все заключения по конкретному пациенту.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список всех заключении по пациенту. </returns>
        Task<IEnumerable<Anamnesis>> GetAnamnesesRepositoryAsync(int patientId);
        /// <summary>
        /// Создать новое заключение для пациента.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="anamnesis"> Заключение. </param>
        /// <returns> В БД добавлено нового заключение для пациента. </returns>
        Task<Anamnesis> CreateAnamnesisRepositoryAsync(int doctorId, int patientId, Anamnesis anamnesis);
    }
}