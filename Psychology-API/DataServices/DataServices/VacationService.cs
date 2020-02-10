using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Repositories;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class VacationService : VacationRepository, IVacationService
    {
        public VacationService(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Vacation>> GetVacationsAsync()
        {
            return await base.GetVacationsRepositoryAsync();
        }

        public async Task<IEnumerable<Vacation>> GetVacationsForDoctorAsync(int doctorId)
        {
            return await base.GetVacationsForDoctorRepositoryAsync(doctorId);
        }
    }
}