using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Репозитории для работы с ролью.
    /// </summary>
    public interface IAdminRepository : IBaseRepository
    {
        /// <summary>
        /// Получить все роли пользователей.
        /// </summary>
        /// <returns> Список ролей. </returns>
        Task<IEnumerable<Role>> GetRolesRepositoryAsync();
        /// <summary>
        /// Создать роль.
        /// </summary>
        /// <param name="role"> Новая роль. </param>
        /// <returns></returns>
        Task<bool> CreateRoleAsync(Role role);
        /// <summary>
        ///  Получить роль из БД.
        /// </summary>
        /// <param name="roleId"> Идентификатор роли. </param>
        /// <returns> Роль. </returns>
        Task<Role> GetRoleRepositoryAsync(int roleId);
    }
}