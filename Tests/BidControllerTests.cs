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
    public class BidControllerTests
    {
        private readonly Mock<IBiddingService> _mockBiddingService;
        private readonly BidController _controller;

        public BidControllerTests()
        {
            _mockBiddingService = new Mock<IBiddingService>();
            _controller = new BidController(_mockBiddingService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "USER")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }


        [Fact]
        public async Task PlaceBid_WithValidData_ReturnsCreated()
        {
            var placeBidDto = new PlaceBidDTO { LotId = Guid.NewGuid(), Amount = 100 };
            var bidId = Guid.NewGuid();

            _mockBiddingService
                .Setup(x => x.PlaceBid(It.IsAny<Guid>(), placeBidDto.LotId, placeBidDto.Amount))
                .ReturnsAsync(bidId);

            var result = await _controller.PlaceBid(placeBidDto);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(BidController.PlaceBid), createdAtResult.ActionName);

            var value = createdAtResult.Value!;
            var property = value.GetType().GetProperty("id");
            Assert.NotNull(property);

            var returnedId = (Guid)property.GetValue(value)!;
            Assert.Equal(bidId, returnedId);
        }




    }

}
