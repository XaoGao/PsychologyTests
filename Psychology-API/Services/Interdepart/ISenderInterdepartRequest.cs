using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Interdepart
{
    /// <summary>
    /// Интерфейс класса по работе с межведомственными запросами.
    /// </summary>
    public interface ISenderInterdepartRequest
    {
        /// <summary>
        /// Создание и отправка межведомственного запроса.
        /// </summary>
        /// <param name="document"> Документ по которому будет проходить межведомственный запрос. </param>
        /// <returns> True если отправка прошла успешно. </returns>
        Task<bool> RequestAsync(Document document);
        /// <summary>
        /// Изменить логику межведомственного запроса.
        /// </summary>
        /// <param name="senderInterdepartRequestFacadKey"> Ключ из справочника по которому произойдет поиск логики
        /// межведомственного запрса
        /// </param>
        void ChangeInterdepartDeprtment(string senderInterdepartRequestFacadKey);
    }
}