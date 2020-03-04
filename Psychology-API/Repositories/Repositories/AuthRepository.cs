using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;
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
        public async Task<Doctor> LoginRepositoryAsync(string username, string password)
        {
            var doctor = await _context.Doctors.Include(d => d.Role).SingleOrDefaultAsync(d => d.Username.Equals(username.ToLower()));

            if (doctor == null)
                return null;

            if (_hash.VerifyPasswordHash(password, doctor.PasswordHash, doctor.PasswordSalt))
                return doctor;

            return null;
        }
        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="doctor"> Врач. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <returns></returns>
        public async Task<Doctor> RegisterRepositoryAsync(Doctor doctor, string password)
        {
            //Приводим логин и пароль к нижнему регистру, чтобы система была регистра не зависимая.
            password = password.ToLower();
            doctor.Username = doctor.Username.ToLower();

            byte[] passwordHash, passwordSalt;

            _hash.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            doctor.PasswordHash = passwordHash;
            doctor.PasswordSalt = passwordSalt;

            await _context.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }
        /// <summary>
        /// Проверка на существование пользователя в системе по логину.
        /// </summary>
        /// <param name="username"> Логин. </param>
        /// <returns> Пользователь с логином уже зарегистрирован. </returns>
        public async Task<bool> UserExistRepositoryAsync(string username)
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
        public async Task<bool> ChangePasswordRepositoryAsync(int doctorId, string newPassword)
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

        public bool VerificateOldPassword(Doctor doctor, string password)
        {
            return _hash.VerifyPasswordHash(password, doctor.PasswordHash, doctor.PasswordSalt);
        }
    }
}