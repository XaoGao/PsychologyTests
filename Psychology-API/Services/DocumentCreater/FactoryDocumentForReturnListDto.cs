using Psychology_API.Dtos.DocumentDto;

namespace Psychology_API.Services.DocumentCreater
{
    /// <summary>
    /// Класс объертка для строителя Документа.
    /// </summary>
    public class FactoryDocumentForReturnListDto
    {
        /// <summary>
        /// Создает документ с инфомацией.
        /// </summary>
        /// <param name="item"> Документ. </param>
        /// <param name="interdepartItem"> Межведомственный запрос. </param>
        /// <returns></returns>
        public DocumentForReturnListDto CreateDocumentFoReturnListDto(DocumentForReturnListDto item,
                                                                    InterdepartRequestForIdDto interdepartItem)
        {
            DocumentForReturnListDtoBuilder documentBuilder = new DocumentForReturnListDtoBuilder();
            var documentDto = documentBuilder.SetDocumentInfo(item).SetInterdepartInfo(interdepartItem).Build();

            return documentDto;
        }
    }
}