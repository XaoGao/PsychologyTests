using System;
using Psychology_Domain.Abstarct;

namespace Psychology_Domain.Domain
{
    /// <summary>
    /// Межведомственный запрос.
    /// </summary>
    public class InterdepartRequest : DomainEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        /// <value></value>
        // public int Id { get; set; }
        /// <summary>
        /// Идентификатор документа по которому идет межведомственный запрос.
        /// </summary>
        /// <value></value>
        public int DocumentId { get; set; }
        /// <summary>
        /// Документ.
        /// </summary>
        /// <value></value>
        public Document Document { get; set; }
        /// <summary>
        /// Дата создания межведоственного запроса.
        /// </summary>
        /// <value></value>
        public DateTime Create { get; set; }
        /// <summary>
        /// Дата обработки межведоственного запроса сторонней системы.
        /// </summary>
        /// <value></value>
        public DateTime Request { get; set; }
        /// <summary>
        /// Дата получения ответа на межведомственный запрос.
        /// </summary>
        /// <value></value>
        public DateTime Response { get; set; }
        /// <summary>
        /// Идентификатор статуса межведомственных запросов.
        /// </summary>
        /// <value></value>
        public int InterdepartStatusId { get; set; }
        /// <summary>
        /// Статус межведомствееного запроса.
        /// </summary>
        /// <value></value>
        public InterdepartStatus InterdepartStatus { get; set; }
        /// <summary>
        /// Создание экземпляра объекта.
        /// </summary>
        /// <param name="documentId"> Идентификатор докумета. </param>
        /// <param name="interdepartStatusId"> Идентификатор статуса межведомственного запроса. </param>
        public InterdepartRequest(Document document, int interdepartStatusId)
        {
            if (document == null)
                throw new ArgumentException("Документ не может быть пустым", nameof(document));

            if (document.Id <= 0)
                throw new ArgumentException("Не валидная ссылка на документ", nameof(document.Id));

            if (interdepartStatusId <= 0)
                throw new ArgumentException("Не валидная ссылка на статус запроса", nameof(interdepartStatusId));

            DocumentId = document.Id;
            // Document = document;
            Create = DateTime.Now;
            InterdepartStatusId = interdepartStatusId;
        }
        public InterdepartRequest()
        {
            
        }
        public InterdepartRequest(int documentId, int interdepartStatusId)
        {
            if (documentId <= 0)
                throw new ArgumentException("Не валидная ссылка на документ", nameof(documentId));

            if (interdepartStatusId <= 0)
                throw new ArgumentException("Не валидная ссылка на статус запроса", nameof(interdepartStatusId));

            DocumentId = documentId;
            InterdepartStatusId = interdepartStatusId;
            Create = DateTime.Now;
            Request = DateTime.Now;
            Response = DateTime.Now;
        }
    }
}