using BlockedCountriesApi.Models;

public interface IBlockedAttemptsLogger
{
    void Log(BlockedAttempt attempt);
    IEnumerable<BlockedAttempt> GetAll(int page, int pageSize);
}
