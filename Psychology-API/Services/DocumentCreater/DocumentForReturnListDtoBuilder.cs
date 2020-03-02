using Psychology_API.Dtos.DocumentDto;

namespace Psychology_API.Services.DocumentCreater
{
    /// <summary>
    /// Строитель для DocumentForReturnListDto, чтобы не раздувать конструктор.
    /// </summary>
    public class DocumentForReturnListDtoBuilder
    {
        /// <summary>
        /// Возвращаемый объект.
        /// </summary>
        private DocumentForReturnListDto documentForReturnListDto;
        /// <summary>
        /// Создание экземпляра класса.
        /// </summary>
        public DocumentForReturnListDtoBuilder()
        {
            documentForReturnListDto = new DocumentForReturnListDto();
        }
        /// <summary>
        /// Добавить в возврощаемый объект инормацию о документе.
        /// </summary>
        /// <param name="document"> Документ.</param>
        /// <returns></returns>
        public DocumentForReturnListDtoBuilder SetDocumentInfo(DocumentForReturnListDto document)
        {
            documentForReturnListDto.Id = document.Id;
            documentForReturnListDto.DocName = document.DocName;
            documentForReturnListDto.Number = document.Number;
            documentForReturnListDto.Series = document.Series;
            documentForReturnListDto.DocumentTypeId = document.DocumentTypeId;
            documentForReturnListDto.DateUpload = document.DateUpload;
            documentForReturnListDto.PatientId = document.PatientId;
            documentForReturnListDto.DocumentType = document.DocumentType;

            return this;
        }
        /// <summary>
        /// Добавить в объект информацию о межведомственных запросах.
        /// </summary>
        /// <param name="interdepart"> Межведомственный запрос. </param>
        /// <returns></returns>
        public DocumentForReturnListDtoBuilder SetInterdepartInfo(InterdepartRequestForIdDto interdepart)
        {
            documentForReturnListDto.InterdepartRequestId = interdepart.InterdepartRequestId;
            documentForReturnListDto.InterdepartStatusId = interdepart.InterdepartStatusId;

            return this;
        }
        /// <summary>
        /// Возвращает документ.
        /// </summary>
        /// <returns></returns>
        public DocumentForReturnListDto Build()
        {
            return documentForReturnListDto;
        }
    }
}