using System;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Psychology_API.Servises.ComputedHash;

namespace Psychology_API.Repositories.Repositories
{
    /// <summary>
    /// Репозитории регистрации и авторизации пользователй в системе.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly DataContext _context;
        /// <summary>
        /// Создание нового экземпляра класса.
        /// </summary>
        /// <param name="context"></param>
        private readonly IHash _hash;
        public AuthRepository(DataContext context, IHash hash)
        {
            _hash = hash;
            _context = context;
        }
        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="username"> Логин пользователя. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <returns> Авторизованый пользователь </returns>
        public async Task<Doctor> LoginAsync(string username, string password)
        {
            var doctor = await _context.Doctors.Include(d => d.Role).SingleOrDefaultAsync(d => d.Username.Equals(username.ToLower()));

            if (doctor == null)
                return null;

            if (_hash.VerifyPasswordHash(password, doctor.PasswordHash, doctor.PasswordSalt))
                return doctor;

            return null;
        }
        /// <summary>
        /// Проверка хэш пароля существующего пользователя с введеным паролем.
        /// </summary>
        /// <param name="password"> Пароль. </param>
        /// <param name="passwordHash"> Хэш пароль пользователя из БД. </param>
        /// <param name="passwordSalt"> Соль пароля из пользователя из БД. </param>
        /// <returns> Пароль и хэш пароль совпадают. </returns>
        // private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        // {
        //     password = password.ToLower();

        //     using(var hmac = new HMACSHA512(passwordSalt))
        //     {
        //         var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //         for (int i = 0; i < computedHash.Length; i++)
        //         {
        //             if (passwordHash[i] != computedHash[i])
        //                 return false;
        //         }
        //     }
        //     return true;
        // }
        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="doctor"> Врач. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <returns></returns>
        public async Task<Doctor> RegisterAsync(Doctor doctor, string password)
        {
            //Приводим логин и пароль к нижнему регистру, чтобы система была регистра не зависимая.
            password = password.ToLower();
            doctor.Username = doctor.Username.ToLower();

            byte[] passwordHash, passwordSalt;

            _hash.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            doctor.PasswordHash = passwordHash;
            doctor.PasswordSalt = passwordSalt;

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }
        /// <summary>
        /// Создание хэш(соль) пароля для нового пользователя.
        /// </summary>
        /// <param name="password"> Пароль. </param>
        /// <param name="passwordHash"> out переменная для хранения хэш пароля. </param>
        /// <param name="passwordSalt"> out переменная для хранения соли хэш пароля. </param>
        // private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        // {
        //     using(var hmac = new HMACSHA512())
        //     {
        //         passwordSalt = hmac.Key;
        //         passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //     }
        // }
        /// <summary>
        /// Проверка на существование пользователя в системе по логину.
        /// </summary>
        /// <param name="username"> Логин. </param>
        /// <returns> Пользователь с логином уже зарегистрирован. </returns>
        public async Task<bool> UserExistAsync(string username)
        {
            if (await _context.Doctors.AnyAsync(d => d.Username.ToLower().Equals(username.ToLower())))
                return true;

            return false;
        }
        /// <summary>
        /// Сменить пароль доктора на новый.
        /// </summary>
        /// <param name="doctorId">  Идентификатор доктора. </param>
        /// <param name="newPassword"> Новый пароль. </param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(int doctorId, string newPassword)
        {
            byte[] passwordHash, passwordSalt;

            var doctorFromRepo = await _context.Doctors.SingleOrDefaultAsync(d => d.Id == doctorId);

            if (doctorFromRepo == null)
                return false;

            _hash.CreatePasswordHash(newPassword.ToLower(), out passwordHash, out passwordSalt);

            doctorFromRepo.PasswordHash = passwordHash;
            doctorFromRepo.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}