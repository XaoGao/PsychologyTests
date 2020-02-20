using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Services.Interdepart;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly ILoggerRepository _loggerRepository;
        private readonly ICache<Document> _cache;
        private readonly IDocumentRepository _documentRepository;
        private readonly ISenderInterdepartRequest _senderInterdepartRequest;

        public DocumentService(DataContext context,
                               ILoggerRepository loggerRepository,
                               ICache<Document> cache,
                               IDocumentRepository documentRepository,
                               ISenderInterdepartRequest senderInterdepartRequest
                               ) : base(context)
        {
            _cache = cache;
            _documentRepository = documentRepository;
            _senderInterdepartRequest = senderInterdepartRequest;
            _loggerRepository = loggerRepository;
            
            Logger += _loggerRepository.WriteInformerLog;
            _documentRepository.GetFromCashe += _cache.Get;
            _documentRepository.SetInCashe += _cache.Set;
            _documentRepository.RemoveItemInCashe += _cache.Remove;
        }

        public void ChangeInterdepartDeprtment(string interdepartDeprtment)
        {
            throw new System.NotImplementedException();
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

        public Task<bool> RequestInterdepart(Document document)
        {
            return _senderInterdepartRequest.Request(document);
        }

        public async Task<bool> SaveDocAsync(Document document, IFormFile formFile)
        {
            return await _documentRepository.SaveDocRepositoryAsync(document, formFile);
        }
    }
}