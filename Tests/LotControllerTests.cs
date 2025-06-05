using BLL.Interfaces;
using DAL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using WebApi.Controllers;

namespace Tests
{
    public class LotControllerTests
    {
        private readonly Mock<ILottingService> _mockLottingService;
        private readonly LotController _controller;

        public LotControllerTests()
        {
            _mockLottingService = new Mock<ILottingService>();
            _controller = new LotController(_mockLottingService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "MANAGER")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }



        [Fact]
        public async Task ConfirmLot_WithValidId_ReturnsOk()
        {
            var lotId = Guid.NewGuid();
            _mockLottingService.Setup(x => x.ChangeLotStatus(lotId, LotStatus.Confirmed))
                .Returns(Task.CompletedTask);

            var result = await _controller.ConfirmLot(lotId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

    }
}