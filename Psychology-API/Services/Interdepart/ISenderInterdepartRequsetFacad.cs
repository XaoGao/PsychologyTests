using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Interdepart
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISenderInterdepartRequestFacad
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task Request(Document document);
    }
}