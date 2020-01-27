using System.Collections.Generic;
using System.IO;
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
        public DocumentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentType>> GetDocTypesAsync()
        {
            var docTypes = await _context.DocumentTypes.ToListAsync();

            return docTypes;
        }

        public async Task<bool> SaveDocAsync(Document document, IFormFile formFile)
        {
            byte[] docBase64 = null;

            using var fileStram = formFile.OpenReadStream();
            using var memoryStream = new MemoryStream();

            await fileStram.CopyToAsync(memoryStream);
            docBase64 = memoryStream.ToArray();

            document.Body = docBase64;

            _context.Documents.Add(document);

            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}