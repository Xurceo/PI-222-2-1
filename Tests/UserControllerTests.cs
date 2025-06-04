using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using WebApi.Controllers;
using Xunit;

namespace Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "ADMIN")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkWithUser()
        {
            var userId = Guid.NewGuid();
            var userDto = new UserDTO { Id = userId };
            _mockUserService.Setup(x => x.GetById(userId)).ReturnsAsync(userDto);

            var result = await _controller.GetById(userId);

            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(userDto, okResult.Value);
        }

        [Fact]
        public async Task UpdateUserPassword_WithValidData_ReturnsNoContent()
        {
            var userId = Guid.NewGuid();
            var newPassword = "newPassword";
            _mockUserService.Setup(x => x.UpdateUserPassword(userId, newPassword))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateUserPassword(userId, newPassword);

            Assert.IsType<NoContentResult>(result);
        }
    }
}