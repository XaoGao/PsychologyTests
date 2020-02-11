using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class VacationService : BaseService, IVacationService
    {
        private readonly IVacationRepository _vacationRepository;
        public VacationService(DataContext context, IVacationRepository vacationRepository) : base(context)
        {
            _vacationRepository = vacationRepository;
        }
        public async Task<IEnumerable<Vacation>> GetVacationsAsync()
        {
            return await _vacationRepository.GetVacationsRepositoryAsync();
        }
        public async Task<IEnumerable<Vacation>> GetVacationsForDoctorAsync(int doctorId)
        {
            return await _vacationRepository.GetVacationsForDoctorRepositoryAsync(doctorId);
        }
    }
}