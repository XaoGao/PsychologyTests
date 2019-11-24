using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.ViewModels;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Телефоный справочник.
    /// </summary>
    public interface IPhonebookRepository
    {
        /// <summary>
        /// Формирует телефоный справочник.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DepartmentWithDoctors>> GetPhonebookAsync();
    }
}