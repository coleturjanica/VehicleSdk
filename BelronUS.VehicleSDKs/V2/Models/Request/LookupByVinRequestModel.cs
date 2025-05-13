using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request;

/// <summary>
/// Represents a request model used to search for a vehicle by its Vin.
/// </summary>
/// <remarks>
/// The following properties are required:
/// <list type="bullet">
/// <item><description>Vin</description></item>
/// </list>
/// </remarks>
public class LookupByVinRequestModel : BaseSdkRequest
{
    [Required]
    public string Vin { get; set; }
}
