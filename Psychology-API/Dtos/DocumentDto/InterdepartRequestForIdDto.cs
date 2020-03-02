using System;

namespace Psychology_API.Dtos.DocumentDto
{
    public class InterdepartRequestForIdDto
    {
        public InterdepartRequestForIdDto(int documentId, int requestId, int statusId)
        {
            if (documentId <= 0)
                throw new ArgumentException(nameof(requestId), "Ссылка на документ не валидная");

            if (requestId <= 0)
                throw new ArgumentException(nameof(requestId), "Ссылка на межведомственный запрос не валидная");

            if (statusId <= 0)
                throw new ArgumentException(nameof(statusId), "Ссылка на  статус межведомственного запроса не валидная");

            InterdepartRequestId = requestId;
            InterdepartStatusId = statusId;
            DocumentId = documentId;
        }
        public int DocumentId { get; set; }
        public int InterdepartRequestId { get; set; }
        public int InterdepartStatusId { get; set; }
    }
}