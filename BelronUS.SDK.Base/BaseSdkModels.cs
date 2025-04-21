using System.ComponentModel.DataAnnotations;

namespace BelronUS.SDK.Base;

public abstract class BaseSdkResponse
{
    [Required]
    public string ApplicationName { get; set; }
    [Required]
    public string CorrelationId { get; set; }
    [Required]
    public Dictionary<string, string> Headers { get; set; }
    [Required]
    public int StatusCode { get; set; }
}

public abstract class BaseSdkRequest
{
    [Required]
    public string ApplicationName { get; set; }
    [Required]
    public string CorrelationId { get; set; }
    [Required]
    public Dictionary<string, string> Headers { get; set; }
    [Required]
    public string ApiBaseUrl { get; set; }
}