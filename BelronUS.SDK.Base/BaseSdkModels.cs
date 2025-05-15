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
    public string BaseApplicationName { get; set; }
    [Required]
    public Guid BaseCorrelationId { get; set; }
    [Required]
    public Dictionary<string, string> BaseHeaders { get; set; }
    [Required]
    public string BaseApiBaseUrl { get; set; }
    [Required]
    public string BaseClientId { get; set; }
    [Required]
    public string BaseClientSecret { get; set; }

    public virtual void Validate()
    {
        RequiredHelper.HasRequired(this);

        if (BaseCorrelationId == Guid.Empty)
    {
        throw new ValidationException("BaseCorrelationId must not be Empty.");
    }
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
    public string BaseApplicationName { get; set; }
    public Guid BaseCorrelationId { get; set; }
    public Dictionary<string, string> BaseHeaders { get; set; }
    public int BaseStatusCode { get; set; }
}