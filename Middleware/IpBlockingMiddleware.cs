using BlockedCountriesApi.Services;
using BlockedCountriesApi.Storage;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BlockedCountriesApi.Middleware
{
    public class IpBlockingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IGeoLocationService _geoLocationService;
        private readonly IBlockedCountryService _countryService;

        public IpBlockingMiddleware(RequestDelegate next,
                                    IGeoLocationService geoLocationService,
                                    IBlockedCountryService countryService)
        {
            _next = next;
            _geoLocationService = geoLocationService;
            _countryService = countryService;
        }

        public async Task Invoke(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrWhiteSpace(ip))
            {
                await _next(context);
                return;
            }

            var info = await _geoLocationService.GetCountryFromIpAsync(ip);
            var countryCode = info?.Country.ToUpper() ?? string.Empty;

            if (_countryService.IsCountryBlocked(countryCode) ||
                AppMemoryStore.TemporalBlocks.ContainsKey(countryCode))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access Denied: Your country is blocked.");
                return;
            }

            await _next(context);
        }
    }
}