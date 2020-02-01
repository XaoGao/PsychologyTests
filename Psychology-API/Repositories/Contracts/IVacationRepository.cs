using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    public interface IVacationRepository : IBaseRepository
    {
        Task<IEnumerable<Vacation>> GetVacations();
        Task<IEnumerable<Vacation>> GetVacationsForDoctor(int doctorId);
    }
}