namespace BlockedCountriesApi.Models
{
    public class TemporalBlock
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public DateTime BlockedUntil { get; set; }
    }
}
