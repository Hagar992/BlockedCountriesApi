namespace BlockedCountriesApi.Models
{
    public class BlockedAttempt
    {
        public string IpAddress { get; set; }
        public string CountryCode { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserAgent { get; set; }
        public bool IsBlocked { get; set; }
    }
}
