using Psychology_API.Services.Token;
using Psychology_Domain.Domain;
using Xunit;

namespace Psychology_XUnit.Services.Token
{
    public class TokenServiceTest
    {
        [Fact]
        public void CreateTokenTest_Success()
        {
            //Given
            var tokenCreater = new TokenService();
            var doctor = new Doctor()
            {
                Id = 1,
                Firstname = "Иван",
                Lastname = "Иванов",
                Middlename = "Иванов",
                RoleId = 1,
                Username = "Иванов_ИИ"
            };
            //When
            var token = tokenCreater.CreateToken(doctor, "test", "10");
            //Then

        }        
    }
}