using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos.DocumentDto;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Services.Interdepart;
using Psychology_API.Services.Cache;
using Psychology_API.Settings.DocumentType;
using Psychology_Domain.Domain;
using Psychology_API.Helpers;
using AutoMapper;

namespace Psychology_API.DataServices.DataServices
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly ILoggerRepository _loggerRepository;
        private readonly ICache<Document> _cache;
        private readonly IDocumentRepository _documentRepository;
        private readonly ISenderInterdepartRequest _senderInterdepartRequest;
        private readonly IMapper _mapper;

        public DocumentService(DataContext context,
                               ILoggerRepository loggerRepository,
                               ICache<Document> cache,
                               IDocumentRepository documentRepository,
                               ISenderInterdepartRequest senderInterdepartRequest,
                               IMapper mapper
                               ) : base(context)
        {
            _cache = cache;
            _documentRepository = documentRepository;
            _senderInterdepartRequest = senderInterdepartRequest;
            _mapper = mapper;
            _loggerRepository = loggerRepository;
            
            Logger += _loggerRepository.WriteInformerLog;
            _documentRepository.GetFromCashe += _cache.Get;
            _documentRepository.SetInCashe += _cache.Set;
            _documentRepository.RemoveItemInCashe += _cache.Remove;
        }

        public void ChangeInterdepartDeprtment(string interdepartDeprtmentKey)
        {
            _senderInterdepartRequest.ChangeInterdepartDeprtment(interdepartDeprtmentKey);
        }

        public async Task<DocumentType> GetDocTypeAsync(int documentId)
        {

            return await _documentRepository.GetDocTypeRepositoryAsync(documentId);
        }

        public async Task<IEnumerable<DocumentType>> GetDocTypesAsync()
        {
            return await _documentRepository.GetDocTypesRepositoryAsync();
        }

        public async Task<Document> GetDocumentAsync(int documentId)
        {            
            return await _documentRepository.GetDocumentRepositoryAsync(documentId);
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync(int patientId)
        {
            return await _documentRepository.GetDocumentsRepositoryAsync(patientId);
        }

        public async Task<bool> RequestInterdepart(Document document)
        {
            return await _senderInterdepartRequest.RequestAsync(document);
        }

        public async Task<bool> SaveDocAsync(Document document, IFormFile formFile)
        {
            return await _documentRepository.SaveDocRepositoryAsync(document, formFile);
        }

        public async Task<IEnumerable<DocumentForReturnListDto>> SetInterdepartId(IEnumerable<Document> documents)
        {
            IEnumerable<InterdepartRequestForIdDto> interdepartRequestDtos =  await GetInterdepartStatus(documents);

            var documentsDto = _mapper.Map<IEnumerable<DocumentForReturnListDto>>(documents);
            
            var documentsForReturn = documentsDto.SetInterdepartIdInDocument(interdepartRequestDtos);
            
            return documentsForReturn;
        }

        public async Task<DocumentForReturnListDto> SetInterdepartId(Document document)
        {
            var interdepart = await _documentRepository.GetInterdepartRequestRepositoryAsync(document.Id);

            var documentForReturn = _mapper.Map<DocumentForReturnListDto>(document);

            if(interdepart != null )
            {
                documentForReturn.InterdepartRequestId = interdepart.Id;
                documentForReturn.InterdepartStatusId = interdepart.InterdepartStatusId;
            }

            return documentForReturn;
        }
        /// <summary>
        /// Вспомогательный метод для получения из БД необходимых межведомственных запросов.
        /// </summary>
        /// <param name="documents"> Список документов. </param>
        /// <returns> Список межведомственных запросов. </returns>
        private async Task<IEnumerable<InterdepartRequestForIdDto>> GetInterdepartStatus(IEnumerable<Document> documents)
        {
            List<InterdepartRequestForIdDto> interdepartRequestDtos = new List<InterdepartRequestForIdDto>();
            foreach (var item in documents)
            {
                if(item.DocumentTypeId != (int)DocumentsType.Pasport)
                    continue;

                var interdepart = await _documentRepository.GetInterdepartRequestRepositoryAsync(item.Id);
                var interdepartForId = new InterdepartRequestForIdDto(interdepart.DocumentId, interdepart.Id, interdepart.InterdepartStatusId);

                interdepartRequestDtos.Add(interdepartForId);
            }
            return interdepartRequestDtos;
        }
    }
}