using BLL.Interfaces;

namespace PI_222_2_1.Api
{
    public static class BidApi
    {
        public static void Serve(WebApplication app)
        {
            var bidApi = app.MapGroup("/api/bids");

            bidApi.MapGet("/", (IBiddingService biddingService) =>
            {
                var bids = biddingService.GetAll();
                return Results.Ok(bids);
            });

            bidApi.MapGet("/{id}", (int id, IBiddingService biddingService) =>
            {
                var bid = biddingService.GetById(id);

                if (bid == null)
                {
                    return Results.NotFound();
                }
                return bid != null ? Results.Ok(bid) : Results.NotFound();
            });

            bidApi.MapGet("/lot/{lotId}", (int lotId, IBiddingService biddingService) =>
            {
                var bids = biddingService.GetBidsByLotId(lotId);

                if (bids == null || !bids.Any())
                {
                    return Results.NotFound();
                }
                return Results.Ok(bids);
            });

            bidApi.MapPost("/", (int lotId, int userId, decimal amount, IBiddingService biddingService) =>
            {
                var id = biddingService.PlaceBid(lotId, userId, amount);
                return Results.Created($"/{id}", null);
            });
        }
    }
}
