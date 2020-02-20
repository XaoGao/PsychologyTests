using System;
using System.Threading.Tasks;
using AutoMapper;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Interdepart
{
    /// <summary>
    /// 
    /// </summary>
    public class SenderInterdepartRequest : ISenderInterdepartRequest
    {
        /// <summary>
        /// 
        /// </summary>
        private ISenderInterdepartRequestFacad _senderInterdepartRequestFacad;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="senderInterdepartRequestFacad"></param>
        public SenderInterdepartRequest(IMapper mapper, IDocumentRepository documentRepository, IServiceProvider serviceProvider)
        {
            _senderInterdepartRequestFacad = new SenderInerdepartRequestFacad(mapper, documentRepository, serviceProvider);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<bool> Request(Document document)
        {
            try
            {
                await _senderInterdepartRequestFacad.Request(document);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="senderInterdepartRequestFacad"></param>
        public void ChangeInterdepartDeprtment(ISenderInterdepartRequestFacad senderInterdepartRequestFacad)
        {
            _senderInterdepartRequestFacad = senderInterdepartRequestFacad;
        }
    }
}