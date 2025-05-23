using BLL.Interfaces;
using BLL.Service;

namespace PI_222_2_1.Api
{
    public static class LotApi
    {
        public static void Serve(WebApplication app)
        {
            var lotApi = app.MapGroup("/api/lots");

            lotApi.MapGet("/", (ILottingService lottingService) =>
            {
                var lots = lottingService.GetAll();
                return Results.Ok(lots);
            });

            lotApi.MapGet("/{id:int}", (int id, ILottingService lottingService) =>
            {
                var lot = lottingService.GetById(id);

                if (lot == null)
                {
                    return Results.NotFound();
                }
                return lot != null ? Results.Ok(lot) : Results.NotFound();
            });

            lotApi.MapGet("/{categoryId}", (int categoryId, ILottingService lottingService) =>
            {
                var lots = lottingService.GetAllByCategoryId((int)categoryId!);

                if (lots == null || !lots.Any())
                {
                    return Results.NotFound();
                }
                return Results.Ok(lots);
            });
        }
    }
}
