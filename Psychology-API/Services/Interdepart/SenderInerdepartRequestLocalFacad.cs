using System.Threading.Tasks;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings.InterdepartStatus;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Interdepart
{
    /// <summary>
    /// 
    /// </summary>
    public class SenderInerdepartRequestLocalFacad : ISenderInterdepartRequestFacad
    {
        private readonly IDocumentRepository _documentRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentRepository"></param>
        public SenderInerdepartRequestLocalFacad(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task Request(Document document)
        {
            InterdepartRequest interdepartRequest;
            if(Verification(document))
            {
                interdepartRequest = new InterdepartRequest(document.Id,
                    (int)InterdepartStatusType.Confirmed);
            }
            else
            {
                interdepartRequest = new InterdepartRequest(document.Id,
                    (int)InterdepartStatusType.Denied);
            }

            _documentRepository.Add(interdepartRequest);
            await _documentRepository.SaveAllAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private bool Verification(Document document)
        {
            if (string.IsNullOrWhiteSpace(document.Number))
                return false;

            if (string.IsNullOrWhiteSpace(document.Series))
                return false;

            if (document.Number.Length != 6 && document.Series.Length != 4)
                return false;

            return true;
        }
    }
}