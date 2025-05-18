using BlockedCountriesApi.Models;
using BlockedCountriesApi.Storage;


public class BlockedAttemptsLogger : IBlockedAttemptsLogger
{
    public void Log(BlockedAttempt attempt)
    {
        AppMemoryStore.BlockedAttempts.Add(attempt);
    }

    public IEnumerable<BlockedAttempt> GetAll(int page, int pageSize)
    {
        return AppMemoryStore.BlockedAttempts
            .OrderByDescending(a => a.Timestamp)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}
