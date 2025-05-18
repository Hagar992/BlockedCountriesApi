public static class ValidationHelper
{
    public static bool IsValidCountryCode(string? code)
    {
        return !string.IsNullOrWhiteSpace(code) &&
               code.Length == 2 &&
               code != "XX" &&
               code.All(char.IsLetter);
    }

    public static bool IsValidDuration(int duration)
    {
        return duration >= 1 && duration <= 1440;
    }

    public static bool IsValidIpAddress(string? ip)
    {
        if (string.IsNullOrWhiteSpace(ip)) return false;

        try
        {
            var ipAddr = System.Net.IPAddress.Parse(ip);
            return ipAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ||
                   ipAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
        }
        catch
        {
            return false;
        }
    }
}