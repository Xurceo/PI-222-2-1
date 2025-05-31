using BLL.Interfaces;
using DAL.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class LotAutoService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

    public LotAutoService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var lottingService = scope.ServiceProvider.GetRequiredService<ILottingService>();
                var lots = await lottingService.GetAll();

                var now = DateTime.UtcNow;
                foreach (var lot in lots.Where(l => (((int)l.Status) < 3) && l.EndTime <= now))
                {
                    try
                    {
                        if (lot.Bids.Count > 0)
                        {
                            await lottingService.ChangeLotStatus(lot.Id, LotStatus.Sold);
                        }
                        else
                        {
                            await lottingService.ChangeLotStatus(lot.Id, LotStatus.Expired);
                        }
                    }
                    catch
                    {
                        // Логування або ігнорування помилок для окремих лотів
                    }
                }
            }
            await Task.Delay(_checkInterval, stoppingToken);
        }
    }
}