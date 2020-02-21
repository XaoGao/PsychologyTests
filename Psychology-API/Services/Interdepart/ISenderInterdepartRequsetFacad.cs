using System.Threading.Tasks;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Services.Interdepart
{
    /// <summary>
    /// Интерфейс по отпраки межведомственного запроса.
    /// </summary>
    public interface ISenderInterdepartRequestFacad<T> where T : DomainEntity
    {
        /// <summary>
        /// Данных к отпрвке через брокера сообщении.
        /// </summary>
        /// <param name="entity"> Данные которые будут отправлены.</param>
        /// <returns></returns>
        Task RequestAsync(T entity);
    }
}