using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Interdepart
{
    public interface ISenderInterdepartRequest
    {
        Task<bool> Request(Document document);
        void ChangeInterdepartDeprtment(ISenderInterdepartRequestFacad senderInterdepartRequestFacad);
    }
}