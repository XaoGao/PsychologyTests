using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Repositories;
using Psychology_API.Services.Token;
using Psychology_API.Servises.ComputedHash;
using Psychology_Domain.Abstarct;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class AuthService : AuthRepository, IAuthService
    {
        private readonly IConfiguration _config;
        public AuthService(DataContext context, IHash hash, IConfiguration config) : base(context, hash)
        {
            _config = config;
        }

        public async Task<bool> ChangePasswordAsync(int doctorId, string newPassword)
        {
            return await base.ChangePasswordRepositoryAsync(doctorId, newPassword);
        }



        public async Task<Doctor> LoginAsync(string username, string password)
        {
            return await base.LoginRepositoryAsync(username, password);
        }

        public async Task<Doctor> RegisterAsync(Doctor doctor, string password)
        {
            return await base.RegisterRepositoryAsync(doctor, password);
        }

        public async Task<bool> UserExistAsync(string username)
        {
            return await base.UserExistRepositoryAsync(username);
        }
        public SecurityToken CreateToken(Doctor doctor)
        {
            var tokenService = new TokenService();
            return tokenService.CreateToken(doctor,
                _config.GetSection("AppSettings:Token").Value,
                _config.GetSection("AppSettings:TimeLifeTokenInHours").Value);
        }
    }
}