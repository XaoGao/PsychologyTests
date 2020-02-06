using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.Cache;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class DocumentRepository : BaseRepository, IDocumentRepository
    {
        private readonly DataContext _context;
        private readonly ICache<Patient> _cache;

        public DocumentRepository(DataContext context, ICache<Patient> cache) : base(context)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<DocumentType>> GetDocTypesAsync()
        {
            var docTypes = await _context.DocumentTypes
                .Where(dt => dt.IsLock == false)
                .ToListAsync();

            return docTypes;
        }

        public async Task<Document> GetDocumentAsync(int documentId)
        {
            var document = await _context.Documents.SingleOrDefaultAsync(d => d.Id == documentId);

            if(document != null)
                _cache.Remove($"{document.PatientId}-Patient");

            return document;
        }

        public async Task<DocumentType> GetDocTypeAsync(int documentId)
        {
            var doc = await _context.Documents.SingleOrDefaultAsync(d => d.Id == documentId);
            var doctype = await _context.DocumentTypes.SingleOrDefaultAsync(dt => dt.Id == doc.DocumentTypeId);

            return doctype;
        }

        public async Task<bool> SaveDocAsync(Document document, IFormFile formFile)
        {
            byte[] docBase64 = null;

            using var fileStram = formFile.OpenReadStream();
            using var memoryStream = new MemoryStream();

            await fileStram.CopyToAsync(memoryStream);
            docBase64 = memoryStream.ToArray();

            document.Body = docBase64;

            _cache.Remove($"{document.PatientId}-Patient");
            _context.Documents.Add(document);

            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}