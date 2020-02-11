using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly ILoggerRepository _loggerRepository;
        private readonly ICache<Document> _cache;
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(DataContext context,
                               ILoggerRepository loggerRepository,
                               ICache<Document> cache,
                               IDocumentRepository documentRepository) : base(context)
        {
            _cache = cache;
            _documentRepository = documentRepository;
            _loggerRepository = loggerRepository;
            Logger += _loggerRepository.WriteInformerLog;
            _documentRepository.RemoveItemInCashe += _cache.Remove;
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
            _documentRepository.GetFromCashe += _cache.Get;
            _documentRepository.SetInCashe += _cache.Set;
            var document = await _documentRepository.GetDocumentRepositoryAsync(documentId);
            _documentRepository.GetFromCashe -= _cache.Get;
            _documentRepository.SetInCashe -= _cache.Set;
            return document;
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync(int patientId)
        {
            return await _documentRepository.GetDocumentsRepositoryAsync(patientId);
        }

        public async Task<bool> SaveDocAsync(Document document, IFormFile formFile)
        {
            _documentRepository.SetInCashe += _cache.Set;
            var save = await _documentRepository.SaveDocRepositoryAsync(document, formFile);
            _documentRepository.SetInCashe -= _cache.Set;
            return save;
        }
    }
}