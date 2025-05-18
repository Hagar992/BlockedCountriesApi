using Microsoft.AspNetCore.Mvc;
using BlockedCountriesApi.Models;
using BlockedCountriesApi.Services;

namespace BlockedCountriesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IBlockedCountryService _countryService;

        public CountriesController(IBlockedCountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: api/countries
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var countries = _countryService.GetAll(page, pageSize, search);
            return Ok(countries);
        }

        // GET: api/countries/{code}
        [HttpGet("{code}")]
        public IActionResult Get(string code)
        {
            if (_countryService.IsCountryBlocked(code))
            {
                var country = _countryService.GetAll(1, 1, code).FirstOrDefault();
                return Ok(country);
            }
            return NotFound(new { Message = "Country is not blocked." });
        }

        // POST: api/countries
        [HttpPost]
        public IActionResult Add([FromBody] BlockedCountry model)
        {
            if (string.IsNullOrWhiteSpace(model.CountryCode) || string.IsNullOrWhiteSpace(model.CountryName))
            {
                return BadRequest("Country code and name are required.");
            }

            _countryService.AddCountry(model.CountryCode, model.CountryName);
            return Ok(new { Message = $"Country {model.CountryCode} added successfully." });
        }

        // DELETE: api/countries/{code}
        [HttpDelete("{code}")]
        public IActionResult Remove(string code)
        {
            var result = _countryService.RemoveCountry(code);
            if (result)
            {
                return Ok(new { Message = $"Country {code} removed successfully." });
            }
            return NotFound(new { Message = $"Country {code} was not found." });
        }
    }
}