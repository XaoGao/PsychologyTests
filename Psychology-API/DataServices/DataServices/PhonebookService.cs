using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Repositories;
using Psychology_API.ViewModels;

namespace Psychology_API.DataServices.DataServices
{
    public class PhonebookService : PhonebookRepository, IPhonebookService
    {
        public PhonebookService(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DepartmentWithDoctors>> GetPhonebookAsync()
        {
            return await base.GetPhonebookRepositoryAsync();
        }
    }
}