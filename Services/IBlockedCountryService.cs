using BlockedCountriesApi.Models;

public interface IBlockedCountryService
{
    void AddCountry(string code, string name);
    bool RemoveCountry(string code);
    IEnumerable<BlockedCountry> GetAll(int page, int pageSize, string? search = null);
    bool IsCountryBlocked(string code);
}
