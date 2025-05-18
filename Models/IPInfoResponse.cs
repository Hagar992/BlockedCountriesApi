namespace BlockedCountriesApi.Models
{
    public class IPInfoResponse
    {
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Org { get; set; } // ISP
        public string Query { get; set; } // IP Address
    }
}
