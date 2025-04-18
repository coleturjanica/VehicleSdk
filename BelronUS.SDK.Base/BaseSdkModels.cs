namespace BelronUS.SDK.Base;

public abstract class BaseSdkResponse
{
    public string ApplicationName { get; set; }
    public string CorrelationId { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public int statusCode { get; set; }
}

public abstract class BaseSdkRequest
{
    public string ApplicationName { get; set; }
    public string CorrelationId { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public string ApiBaseUrl { get; set; }
}