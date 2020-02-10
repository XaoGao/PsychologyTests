using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис автризации.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="username"> Логин пролзователя. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <returns></returns>
        Task<Doctor> LoginAsync(string username, string password);
        /// <summary>
        /// Регистрация пользователя в системе.
        /// </summary>
        /// <param name="doctor"> Врач. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <returns></returns>
        Task<Doctor> RegisterAsync(Doctor doctor, string password);
        /// <summary>
        /// Проверка на существование пользователя в системе по логину.
        /// </summary>
        /// <param name="username"> Логни. </param>
        /// <returns> Пользователь с логином уже зарегистрирован. </returns>
        Task<bool> UserExistAsync(string username);
        /// <summary>
        /// Сменить пароль доктора на новый.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="newPassword"> Новый пароль. </param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(int doctorId, string newPassword);
        /// <summary>
        /// Сформировать токен для пользователя.
        /// </summary>
        /// <param name="doctor"> Идентификатор пользователя. </param>
        /// <returns> JWT объект. </returns>
        SecurityToken CreateToken(Doctor doctor);

    }
}