using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Services.Token;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _authRepository;

        public AuthService(DataContext context, 
                           IConfiguration config,
                           IAuthRepository authRepository)
        {
            _config = config;
            _authRepository = authRepository;
        }

        public async Task<bool> ChangePasswordAsync(int doctorId, string newPassword)
        {
            return await _authRepository.ChangePasswordRepositoryAsync(doctorId, newPassword);
        }
        public async Task<Doctor> LoginAsync(string username, string password)
        {
            return await _authRepository.LoginRepositoryAsync(username, password);
        }
        public async Task<Doctor> RegisterAsync(Doctor doctor, string password)
        {
            return await _authRepository.RegisterRepositoryAsync(doctor, password);
        }
        public async Task<bool> UserExistAsync(string username)
        {
            return await _authRepository.UserExistRepositoryAsync(username);
        }
        public SecurityToken CreateToken(Doctor doctor)
        {
            var tokenService = new TokenService();

            var keyForToken = _config.GetSection("AppSettings:Token").Value;
            var timeLifeToken = _config.GetSection("AppSettings:TimeLifeTokenInHours").Value;

            return tokenService.CreateToken(doctor, keyForToken, timeLifeToken);
        }

        public bool VerificateOldPassword(Doctor doctor, string password)
        {
            return _authRepository.VerificateOldPassword(doctor, password);
        }
    }
}