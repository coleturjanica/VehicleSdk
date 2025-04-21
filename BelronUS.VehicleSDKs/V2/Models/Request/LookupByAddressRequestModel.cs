using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request;


/// <summary>
/// Represents the fields involved in searching for a vehicle by an address.
/// </summary>
/// <remarks>
/// The following fields are required:
/// <list type="bullet">
/// <item><description>LastName</description></item>
/// <item><description>RegisteredStreetAddress</description></item>
/// <item><description>State</description></item>
/// <item><description>Zip</description></item>
/// </list>
/// </remarks>
public class LookupByAddressRequestModel : BaseSdkRequest
{
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string RegisteredStreetAddress { get; set; }
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string Zip { get; set; }
}