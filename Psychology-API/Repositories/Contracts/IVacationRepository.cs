using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVacationRepository : IBaseRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Vacation>> GetVacationsRepositoryAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        Task<IEnumerable<Vacation>> GetVacationsForDoctorRepositoryAsync(int doctorId);
    }
}