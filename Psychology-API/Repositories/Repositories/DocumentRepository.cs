using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class DocumentRepository : BaseRepository, IDocumentRepository
    {
        private readonly DataContext _context;
        private const string suffix = "-Document";
        public event Action<string, string, Document> SetInCashe;
        public event Func<string, string, Document> GetFromCashe;
        public event Action<string, string> RemoveItemInCashe;

        public DocumentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentType>> GetDocTypesRepositoryAsync()
        {
            var docTypes = await _context.DocumentTypes
                .Where(dt => dt.IsLock == false)
                .ToListAsync();

            return docTypes;
        }

        public async Task<Document> GetDocumentRepositoryAsync(int documentId)
        {
            Document document = GetFromCashe(documentId.ToString(), suffix);

            if(document == null)
            {
                document = await _context.Documents.SingleOrDefaultAsync(d => d.Id == documentId);

                if(document != null)
                    SetInCashe(document.Id.ToString(), suffix, document);
            }

            return document;
        }

        public async Task<DocumentType> GetDocTypeRepositoryAsync(int documentId)
        {
            var doc = await _context.Documents.SingleOrDefaultAsync(d => d.Id == documentId);
            var doctype = await _context.DocumentTypes.SingleOrDefaultAsync(dt => dt.Id == doc.DocumentTypeId);

            return doctype;
        }

        public async Task<bool> SaveDocRepositoryAsync(Document document, IFormFile formFile)
        {
            byte[] docBase64 = null;

            using var fileStram = formFile.OpenReadStream();
            using var memoryStream = new MemoryStream();

            await fileStram.CopyToAsync(memoryStream);
            docBase64 = memoryStream.ToArray();

            document.Body = docBase64;

            _context.Documents.Add(document);

            if (await _context.SaveChangesAsync() > 0)
            {
                RemoveItemInCashe(document.PatientId.ToString(), "-Patient");
                SetInCashe(document.Id.ToString(), suffix, document);
                return true;
            }
                
            return false;
        }

        public async Task<IEnumerable<Document>> GetDocumentsRepositoryAsync(int patientId)
        {
            var documents = await _context.Documents.Where(d => d.PatientId == patientId).ToListAsync();

            return documents;
        }
        public override void Remove<T>(T entity)
        {
            var document = entity as Document;
            RemoveItemInCashe(document.Id.ToString(), suffix);
            base.Remove(document);
        }
    }
}