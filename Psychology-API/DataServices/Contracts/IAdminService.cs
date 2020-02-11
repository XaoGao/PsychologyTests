using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис для администрировании с пользователями.
    /// </summary>
    public interface IAdminService : IBaseService
    {
        /// <summary>
        /// Получить все роли пользователей.
        /// </summary>
        /// <returns> Список ролей. </returns>
        Task<IEnumerable<Role>> GetRolesAsync();
        /// <summary>
        /// Получить всех докторов в системе.
        /// </summary>
        /// <returns> Список докторов. </returns>
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        /// <summary>
        /// Создать роль.
        /// </summary>
        /// <param name="role"> Роль. </param>
        /// <returns></returns>
        Task<bool> CreateRoleAsync(Role role);
        /// <summary>
        /// Получить роль.
        /// </summary>
        /// <param name="roleId"> Идентификатор роли. </param>
        /// <returns> Роль. </returns>
        Task<Role> GetRoleAsync(int roleId);
    }
}