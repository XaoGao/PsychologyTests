using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Интерфейс репозитория пациента.
    /// </summary>
    public interface IPatientRepository : IBaseRepository
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
        /// Получить конкретного пациента доктора без добовления в кеш.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns></returns>
        Task<Patient> GetPatientWithoutCacheAsync(int doctorId, int patientId);
        /// <summary>
        /// Перевести пациента в архив.
        /// </summary>
        /// <param name="patient"> Пациент. </param>
        /// <returns></returns>
        void MovePatinetToArchive(Patient patient);
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
        /// <summary>
        /// Получить всех пациентов для регистратора.
        /// </summary>
        /// <returns> Список все пациентов. </returns>
        Task<IEnumerable<Patient>> GetPatientsForRegistryAsync();
    }
}