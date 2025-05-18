using BlockedCountriesApi.Models;

namespace BlockedCountriesApi.Services
{
    public interface IGeoLocationService
    {
        Task<IPInfoResponse?> GetCountryFromIpAsync(string ipAddress);
    }
}
