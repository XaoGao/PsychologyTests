using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDoctorService : IBaseService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        Task<Doctor> GetDoctorAsync(int doctorId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        Task<Doctor> GetDoctorWithoutCacheAsync(int doctorId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Doctor>> GetDoctorsAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        Task<IEnumerable<Reception>> GetReceptionsForDoctorsAsync(int doctorId);
    }
}