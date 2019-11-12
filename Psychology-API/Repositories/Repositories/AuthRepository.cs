using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public Task<Doctor> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<Doctor> Register(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}