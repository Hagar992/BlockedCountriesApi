using BlockedCountriesApi.Models;
using System.Collections.Concurrent;

namespace BlockedCountriesApi.Storage {
    public static class AppMemoryStore
    {
        public static ConcurrentDictionary<string, BlockedCountry> BlockedCountries { get; } = new();
        public static ConcurrentDictionary<string, TemporalBlock> TemporalBlocks { get; } = new();
        public static ConcurrentBag<BlockedAttempt> BlockedAttempts { get; } = new();
    }
}

