using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис для работы с документами.
    /// </summary>
    public interface IDocumentService : IBaseService
    {
        /// <summary>
        /// Перевести входящую обертку IFormFile в массив байтов и сохранить в базу данных.
        /// </summary>
        /// <param name="document"> Документа. </param>
        /// <param name="formFile"> Обертка входящего документа. </param>
        /// <returns> Перевсти документа в массив байтов, и сохранить в базу данных. </returns>
        Task<bool> SaveDocAsync(Document document, IFormFile formFile);
        /// <summary>
        /// Получить все категории документов.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DocumentType>> GetDocTypesAsync();
        /// <summary>
        /// Получить документ из репозитория.
        /// </summary>
        /// <param name="documentId"> Идентификатор документа. </param>
        /// <returns> Документ типа Document </returns>
        Task<Document> GetDocumentAsync(int documentId);
        /// <summary>
        /// Получить категорию документа.
        /// </summary>
        /// <param name="documentId"> Идентификатор документа. </param>
        /// <returns> Категория документа. </returns>
        Task<DocumentType> GetDocTypeAsync(int documentId);
        /// <summary>
        /// Получить список документов пациента.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список документов. </returns>
        Task<IEnumerable<Document>> GetDocumentsAsync(int patientId);
    }
}