﻿namespace BlockedCountriesApi.Models
{
    public class BlockedCountry
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public DateTime BlockedAt { get; set; }
    }
}
