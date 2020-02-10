using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Репозитории для работы с документами.
    /// </summary>
    public interface IDocumentRepository : IBaseRepository, ILoggerable, ICasheable<Document>
    {
        /// <summary>
        /// Перевести входящую обертку IFormFile в массив байтов и сохранить в базу данных.
        /// </summary>
        /// <param name="document"> Документа. </param>
        /// <param name="formFile"> Обертка входящего документа. </param>
        /// <returns> Перевсти документа в массив байтов, и сохранить в базу данных. </returns>
        Task<bool> SaveDocRepositoryAsync(Document document, IFormFile formFile);
        /// <summary>
        /// Получить все категории документов.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DocumentType>> GetDocTypesRepositoryAsync();
        /// <summary>
        /// Получить документ из БД.
        /// </summary>
        /// <param name="documentId"> Идентификатор документа. </param>
        /// <returns> Документ типа Document </returns>
        Task<Document> GetDocumentRepositoryAsync(int documentId);
        /// <summary>
        /// Получить категорию документа.
        /// </summary>
        /// <param name="documentId"> Идентификатор документа. </param>
        /// <returns></returns>
        Task<DocumentType> GetDocTypeRepositoryAsync(int documentId);
        /// <summary>
        /// Получить список документов из БД.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список документов. </returns>
        Task<IEnumerable<Document>> GetDocumentsRepositoryAsync(int patientId);
    }
}