using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис для работв с заключениями пациентов.
    /// </summary>
    public interface IAnamnesisService
    {
        /// <summary>
        /// Получить все заключения по конкретному пациенту.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список всех заключении по пациенту. </returns>
        Task<IEnumerable<Anamnesis>> GetAnamnesesAsync(int patientId);
        /// <summary>
        /// Создать новое заключение для пациента.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="anamnesis"> Заключение. </param>
        /// <returns> В БД добавлено нового заключение для пациента. </returns>
        Task<Anamnesis> CreateAnamnesisAsync(int doctorId, int patientId, Anamnesis anamnesis);
    }
}