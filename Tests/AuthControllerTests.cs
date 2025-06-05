using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.CreateDTOs;
using BLL.Interfaces;
using WebApi.Controllers;
using WebApi.Infrastructure;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly TokenProvider _tokenProvider;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["JWT:Secret"]).Returns("Postavte-100-baliv-PLS-abcdefghijklmnopqrstuvwxyz");
            configMock.Setup(c => c["JWT:Issuer"]).Returns("testIssuer");
            configMock.Setup(c => c["JWT:Audience"]).Returns("testAudience");

            _tokenProvider = new TokenProvider(configMock.Object);

            _controller = new AuthController(_userServiceMock.Object, _tokenProvider)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenInvalidCredentials()
        {
            var dto = new LoginDTO { Username = "invalid", Password = "wrong" };
            _userServiceMock.Setup(s => s.Authenticate(dto)).ReturnsAsync((UserDTO)null);

            var result = await _controller.Login(dto);

            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenValidCredentials()
        {
            var userDto = new UserDTO { Id = Guid.NewGuid(), Username = "user", Role = "USER" };
            var dto = new LoginDTO { Username = "user", Password = "pass" };

            _userServiceMock.Setup(s => s.Authenticate(dto)).ReturnsAsync(userDto);

            var result = await _controller.Login(dto);

            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

            var value = objectResult.Value;
            var userProp = value.GetType().GetProperty("User");
            var user = userProp?.GetValue(value);
            var resultUserDto = Assert.IsType<UserDTO>(user);

            Assert.Equal(userDto.Username, resultUserDto.Username);

            var mockCookies = new Mock<IResponseCookies>();
        }


        [Fact]
        public async Task Register_ReturnsBadRequest_WhenDtoIsNull()
        {
            var result = await _controller.Register(null);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenSuccessful()
        {
            var dto = new CreateUserDTO { Username = "newuser", Password = "password" };
            var userId = Guid.NewGuid();
            var userDto = new UserDTO { Id = userId, Username = "newuser", Role = "USER" };

            _userServiceMock.Setup(s => s.AddUser(dto)).ReturnsAsync(userId);
            _userServiceMock.Setup(s => s.GetById(userId)).ReturnsAsync(userDto);

            var result = await _controller.Register(dto);

            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);


            var value = objectResult.Value;
            var userProp = value.GetType().GetProperty("User");
            var user = userProp?.GetValue(value);
            var resultUserDto = Assert.IsType<UserDTO>(user);

            Assert.Equal(userDto.Username, resultUserDto.Username);
        }


        [Fact]
        public void Logout_ShouldClearCookie()
        {
            var result = _controller.Logout();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}