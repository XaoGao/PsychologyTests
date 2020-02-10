using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Repositories;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public class ReceptionService : ReceptionRepository, IReceptionService
    {
        public ReceptionService(DataContext context) : base(context)
        {
        }

        public async Task<bool> CheckReceptionTimeAsync(int doctorId, DateTime timeReception)
        {
            return await base.CheckReceptionTimeRepositoryAsync(doctorId, timeReception);
        }

        public async Task<IEnumerable<DateTime>> GetFreeReceptionTimeForDayAsync(int doctorId, DateTime dateTimeReception)
        {
            return await base.GetFreeReceptionTimeForDayRepositoryAsync(doctorId, dateTimeReception);
        }

        public async Task<IEnumerable<Reception>> GetReseptionsAsync(int doctorId)
        {
            return await base.GetReseptionsRepositoryAsync(doctorId);
        }

        public async Task<IEnumerable<Reception>> GetReseptionsOfCurrentWeekAsync(int doctorId, DateTime now)
        {
            return await base.GetReseptionsOfCurrentWeekRepositoryAsync(doctorId, now);
        }
    }
}