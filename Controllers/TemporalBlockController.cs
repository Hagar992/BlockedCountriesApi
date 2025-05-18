using Microsoft.AspNetCore.Mvc;
using BlockedCountriesApi.Models;
using BlockedCountriesApi.Services;
using BlockedCountriesApi.Storage;
using System.Globalization;

namespace BlockedCountriesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemporalBlockController : ControllerBase
    {
        private readonly IBlockedCountryService _countryService;

        public TemporalBlockController(IBlockedCountryService countryService)
        {
            _countryService = countryService;
        }

        // POST: api/temporalblock
        [HttpPost]
        public IActionResult AddTemporalBlock([FromBody] TemporalBlockRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CountryCode))
            {
                return BadRequest(new { Message = "Country code is required." });
            }

            var countryCode = request.CountryCode.ToUpper();

            if (countryCode.Length != 2 || countryCode == "XX")
            {
                return BadRequest(new { Message = "Invalid country code. Must be a valid 2-letter ISO code." });
            }

            if (request.DurationMinutes < 1 || request.DurationMinutes > 1440)
            {
                return BadRequest(new { Message = "Duration must be between 1 and 1440 minutes." });
            }

            if (AppMemoryStore.TemporalBlocks.ContainsKey(countryCode))
            {
                return Conflict(new { Message = $"Country {countryCode} is already temporarily blocked." });
            }

            var temporalBlock = new TemporalBlock
            {
                CountryCode = countryCode,
                CountryName = new RegionInfo(countryCode).EnglishName, // Optional: requires System.Globalization
                BlockedUntil = DateTime.UtcNow.AddMinutes(request.DurationMinutes)
            };

            AppMemoryStore.TemporalBlocks.TryAdd(countryCode, temporalBlock);

            return Ok(new
            {
                Message = $"Country {countryCode} blocked for {request.DurationMinutes} minute(s).",
                Until = temporalBlock.BlockedUntil
            });
        }
    }

    public class TemporalBlockRequest
    {
        public string CountryCode { get; set; } = "";
        public int DurationMinutes { get; set; }
    }
}