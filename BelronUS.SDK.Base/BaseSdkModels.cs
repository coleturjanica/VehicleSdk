using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base.Helpers;

namespace BelronUS.SDK.Base;

/// <summary>
/// Represents the base response model from any API call.
/// </summary>
/// <remarks>
/// The following properties are required:
/// <list type="bullet">
/// <item><description>ApplicationName</description></item>
/// <item><description>CorrelationId</description></item>
/// <item><description>Headers</description></item>
/// <item><description>ApiBaseUrl</description></item>
/// </list>
/// </remarks>
public abstract class BaseSdkRequest
{
    [Required]
    public string ApplicationName { get; set; }
    [Required]
    public Guid CorrelationId { get; set; }
    [Required]
    public Dictionary<string, string> Headers { get; set; }
    [Required]
    public string ApiBaseUrl { get; set; }
    [Required]
    public string ClientId { get; set; }
    [Required]
    public string ClientSecret { get; set; }

    public virtual void Validate()
    {
        RequiredHelper.HasRequired(this);
    }
}

/// <summary>
/// Represents the base request model used in any API call.
/// </summary>
/// <remarks>
/// The following properties are required:
/// <list type="bullet">
/// <item><description>ApplicationName</description></item>
/// <item><description>CorrelationId</description></item>
/// <item><description>Headers</description></item>
/// <item><description>StatusCode</description></item>
/// </list>
/// </remarks>
public abstract class BaseSdkResponse
{
    public string ApplicationName { get; set; }
    public Guid CorrelationId { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public int StatusCode { get; set; }
}