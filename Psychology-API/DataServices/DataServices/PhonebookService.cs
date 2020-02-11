using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.ViewModels;

namespace Psychology_API.DataServices.DataServices
{
    public class PhonebookService : IPhonebookService
    {
        private readonly IPhonebookRepository _phonebookRepository;
        public PhonebookService(IPhonebookRepository phonebookRepository)
        {
            _phonebookRepository = phonebookRepository;
        }

        public async Task<IEnumerable<DepartmentWithDoctors>> GetPhonebookAsync()
        {
            return await _phonebookRepository.GetPhonebookRepositoryAsync();
        }
    }
}