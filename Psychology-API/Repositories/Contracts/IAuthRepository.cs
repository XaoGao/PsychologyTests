using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    public interface IAuthRepository
    {
        Task<Doctor> Login(string username, string password);
        Task<Doctor> Register(string username, string password);
    }
}