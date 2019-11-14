using System.Threading.Tasks;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Идентификация пользователя в системе.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="username"> Логин пролзователя. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <returns></returns>
        Task<Doctor> Login(string username, string password);
        /// <summary>
        /// Решистрация пользователя в системе.
        /// </summary>
        /// <param name="doctor"> Врач. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <returns></returns>
        Task<Doctor> Register(Doctor doctor, string password);
    }
}