using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Psychology_Domain.Domain;

namespace Psychology_API.Services.Token
{
    /// <summary>
    /// Класс по формированию токенов.
    /// </summary>
    public class TokenService
    {
        /// <summary>
        /// Создание экземпляра класса.
        /// </summary>
        public TokenService()
        {
            
        }
        /// <summary>
        /// Формирование токенов.
        /// </summary>
        /// <param name="doctor"> Доктор, данные которого будут помещены в токен. </param>
        /// <param name="secret"> Секретный ключ для шифрования. </param>
        /// <param name="timeLifeToken"> Время жизни токена в часах. </param>
        /// <returns> Токен. </returns>
        public SecurityToken CreateToken(Doctor doctor, string secret, string timeLifeToken)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, doctor.Id.ToString()),
                new Claim(ClaimTypes.Name, doctor.Username),
                new Claim(ClaimTypes.Role, doctor.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(int.Parse(timeLifeToken)),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}