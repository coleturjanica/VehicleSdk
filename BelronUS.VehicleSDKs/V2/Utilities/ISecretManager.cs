namespace BelronUS.VehicleSDKs.V2.Utilities;

public interface ISecretManager
{
    public string GetBelronApiBaseURL();
    public string GetOriginVerifyKey();
    public string GetOriginVerifySecret();
}

public class SecretManager : ISecretManager
{
    #region Secrets
    private const string BELRON_API_BASE_URL = nameof(BELRON_API_BASE_URL);
    private const string ORIGIN_VERIFY_KEY = nameof(ORIGIN_VERIFY_KEY);
    private const string ORIGIN_VERIFY_SECRET = nameof(ORIGIN_VERIFY_SECRET);
    #endregion

    public string GetBelronApiBaseURL()
    {
        return GetEnvironmentVariable(BELRON_API_BASE_URL);
    }
    public string GetOriginVerifyKey()
    {
        return GetEnvironmentVariable(ORIGIN_VERIFY_KEY);
    }
    public string GetOriginVerifySecret()
    {
        return GetEnvironmentVariable(ORIGIN_VERIFY_SECRET);
    }
    #region Private Methods
    /// <summary>
    /// Used to pull environment variable values, Exception thrown if the value is empty or null.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static string GetEnvironmentVariable(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        return !string.IsNullOrEmpty(value) ? value : throw new InvalidOperationException($"{name} is not an environment variable.");
    }

    #endregion
}
