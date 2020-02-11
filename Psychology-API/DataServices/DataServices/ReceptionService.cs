using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class ReceptionService : BaseService, IReceptionService
    {
        private readonly IReceptionRepository _receptionRepository;
        public ReceptionService(DataContext context, IReceptionRepository receptionRepository) : base(context)
        {
            _receptionRepository = receptionRepository;
        }
        public async Task<bool> CheckReceptionTimeAsync(int doctorId, DateTime timeReception)
        {
            return await _receptionRepository.CheckReceptionTimeRepositoryAsync(doctorId, timeReception);
        }
        public async Task<IEnumerable<DateTime>> GetFreeReceptionTimeForDayAsync(int doctorId, DateTime dateTimeReception)
        {
            return await _receptionRepository.GetFreeReceptionTimeForDayRepositoryAsync(doctorId, dateTimeReception);
        }
        public async Task<IEnumerable<Reception>> GetReseptionsAsync(int doctorId)
        {
            return await _receptionRepository.GetReseptionsRepositoryAsync(doctorId);
        }
        public async Task<IEnumerable<Reception>> GetReseptionsOfCurrentWeekAsync(int doctorId, DateTime now)
        {
            return await _receptionRepository.GetReseptionsOfCurrentWeekRepositoryAsync(doctorId, now);
        }
    }
}