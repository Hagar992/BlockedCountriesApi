using BlockedCountriesApi.Models;
using System.Collections.Concurrent;
using BlockedCountriesApi.Storage;


namespace BlockedCountriesApi.Services
{
    public class BlockedCountryService : IBlockedCountryService
    {
        public void AddCountry(string countryCode, string countryName)
        {
            if (!AppMemoryStore.BlockedCountries.ContainsKey(countryCode))
            {
                var blockedCountry = new BlockedCountry
                {
                    CountryCode = countryCode.ToUpper(),
                    CountryName = countryName,
                    BlockedAt = DateTime.UtcNow
                };

                AppMemoryStore.BlockedCountries.TryAdd(countryCode, blockedCountry);
            }
        }

        public bool RemoveCountry(string countryCode)
        {
            return AppMemoryStore.BlockedCountries.TryRemove(countryCode.ToUpper(), out _);
        }

        public IEnumerable<BlockedCountry> GetAll(int page, int pageSize, string? search = null)
        {
            var blockedList = AppMemoryStore.BlockedCountries.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                blockedList = blockedList.Where(c =>
                    c.CountryCode.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    c.CountryName.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return blockedList
                .OrderBy(c => c.CountryCode)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public bool IsCountryBlocked(string countryCode)
        {
            return AppMemoryStore.BlockedCountries.ContainsKey(countryCode.ToUpper());
        }
    }
}
