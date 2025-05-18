using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using BlockedCountriesApi.Storage;

namespace BlockedCountriesApi.BackgroundTasks
{
    public class TemporalBlockCleaner : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                var expired = AppMemoryStore.TemporalBlocks
                    .Where(pair => pair.Value.BlockedUntil <= now)
                    .Select(pair => pair.Key)
                    .ToList();

                foreach (var countryCode in expired)
                {
                    AppMemoryStore.TemporalBlocks.TryRemove(countryCode, out _);
                    Console.WriteLine($"[BackgroundService] Unblocked {countryCode} (expired)");
                }

               
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
