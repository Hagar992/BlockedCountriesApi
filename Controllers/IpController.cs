using Microsoft.AspNetCore.Mvc;
using BlockedCountriesApi.Models;
using BlockedCountriesApi.Services;
using System.Globalization;
using BlockedCountriesApi.Storage;

namespace BlockedCountriesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IpController : ControllerBase
    {
        private readonly IGeoLocationService _geoLocationService;
        private readonly IBlockedCountryService _countryService;

        public IpController(IGeoLocationService geoLocationService, IBlockedCountryService countryService)
        {
            _geoLocationService = geoLocationService;
            _countryService = countryService;
        }

        // GET: api/ip/lookup?ipAddress=8.8.8.8
        [HttpGet("lookup")]
        public async Task<IActionResult> GetCountryFromIp([FromQuery] string? ipAddress = null)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return BadRequest("IP address is not provided or could be determined.");
            }

            var info = await _geoLocationService.GetCountryFromIpAsync(ipAddress);

            if (info == null)
            {
                return StatusCode(500, "Failed to retrieve geolocation data.");
            }

            return Ok(new
            {
                IpAddress = ipAddress,
                CountryCode = info.Country,
                CountryName = info.CountryName,
                Region = info.Region,
                City = info.City,
                ISP = info.Org
            });
        }

        // GET: api/ip/check-block
        [HttpGet("check-block")]
        public async Task<IActionResult> CheckIpBlocked()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var userAgent = Request.Headers.UserAgent.ToString();

            var info = await _geoLocationService.GetCountryFromIpAsync(ip);
            var countryCode = info?.Country.ToUpper() ?? "Unknown";

            bool isBlocked = false;

            if (!string.IsNullOrEmpty(countryCode))
            {
                isBlocked = _countryService.IsCountryBlocked(countryCode) ||
                            AppMemoryStore.TemporalBlocks.ContainsKey(countryCode);
            }

            var attempt = new BlockedAttempt
            {
                IpAddress = ip,
                CountryCode = countryCode,
                Timestamp = DateTime.UtcNow,
                UserAgent = userAgent,
                IsBlocked = isBlocked
            };

            AppMemoryStore.BlockedAttempts.Add(attempt);

            return Ok(new
            {
                IpAddress = ip,
                CountryCode = countryCode,
                IsBlocked = isBlocked
            });
        }
    }
}