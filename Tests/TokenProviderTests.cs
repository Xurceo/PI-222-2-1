using BLL.DTOs;
using Microsoft.Extensions.Configuration;
using Moq;
using WebApi.Infrastructure;
using Xunit;

namespace Tests
{
    public class TokenProviderTests
    {
        [Fact]
        public void GenerateToken_ReturnsValidToken()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["JWT:Secret"]).Returns("this_is_a_very_secure_and_long_secret_key_123!");
            configMock.Setup(x => x["JWT:Issuer"]).Returns("test");
            configMock.Setup(x => x["JWT:Audience"]).Returns("test");

            var tokenProvider = new TokenProvider(configMock.Object);
            var userDto = new UserDTO { Id = Guid.NewGuid(), Username = "test", Role = "USER" };

            var token = tokenProvider.GenerateToken(userDto);

            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
    }
}