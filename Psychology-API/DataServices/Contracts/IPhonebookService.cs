using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.ViewModels;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис для работы с телефоныыс справочником.
    /// </summary>
    public interface IPhonebookService
    {
        /// <summary>
        /// Формирует телефоный справочник.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DepartmentWithDoctors>> GetPhonebookAsync();
    }
}