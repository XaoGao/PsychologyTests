using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Repositories.Repositories;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class DocumentService : DocumentRepository, IDocumentService
    {
        private readonly ILoggerRepository _loggerRepository;
        private readonly ICache<Document> _cache;

        public DocumentService(DataContext context,
                               ILoggerRepository loggerRepository,
                               ICache<Document> cache) : base(context)
        {
            _cache = cache;
            _loggerRepository = loggerRepository;
            Logger += _loggerRepository.WriteInformerLog;
        }

        public async Task<DocumentType> GetDocTypeAsync(int documentId)
        {

            return await base.GetDocTypeRepositoryAsync(documentId);
        }

        public async Task<IEnumerable<DocumentType>> GetDocTypesAsync()
        {
            return await base.GetDocTypesRepositoryAsync();
        }

        public async Task<Document> GetDocumentAsync(int documentId)
        {
            var document = await base.GetDocumentRepositoryAsync(documentId);
            return document;
        }

        public async Task<bool> SaveDocAsync(Document document, IFormFile formFile)
        {
            var save = await base.SaveDocRepositoryAsync(document, formFile);
            return save;
        }
    }
}