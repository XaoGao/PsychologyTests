using System.Collections.Generic;
using System.Linq;
using Psychology_API.Dtos.DocumentDto;
using Psychology_API.Services.DocumentCreater;

namespace Psychology_API.Helpers
{
    public static class DocumentForReturnListDtoExtension
    {
        public static List<DocumentForReturnListDto> SetInterdepartIdInDocument(this List<DocumentForReturnListDto> list,
                                                                                List<InterdepartRequestForIdDto> interdepartRequestDtos)
        {
            List<DocumentForReturnListDto> listWithInterDepartId = new List<DocumentForReturnListDto>();
            FactoryDocumentForReturnListDto documentFactory = new FactoryDocumentForReturnListDto();

            foreach (var item in list)
            {
                var interdepartItem = interdepartRequestDtos.Where(ii => ii.DocumentId == item.Id).FirstOrDefault();

                var documentDto = documentFactory.CreateDocumentFoReturnListDto(item, interdepartItem);

                listWithInterDepartId.Add(documentDto);
            }

            return listWithInterDepartId;
        }
    }
}