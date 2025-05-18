using Newtonsoft.Json;
using BlockedCountriesApi.Models;

namespace BlockedCountriesApi.Services
{
    public class GeoLocationService : IGeoLocationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeoLocationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IPInfoResponse?> GetCountryFromIpAsync(string ipAddress)
        {
            var baseUrl = _configuration["IpGeoApi:BaseUrl"];
            var apiKey = _configuration["IpGeoApi:ApiKey"];

            var fullUrl = $"{baseUrl}/{ipAddress}/json/?key={apiKey}";

            var response = await _httpClient.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            var ipInfo = JsonConvert.DeserializeObject<IPInfoResponse>(json);

            return ipInfo;
        }
    }
}
