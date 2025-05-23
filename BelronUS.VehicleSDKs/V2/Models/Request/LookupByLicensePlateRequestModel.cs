
using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request
{
    /// <summary>
    /// Represents a request model used to search for a vehicle by its license plate.
    /// </summary>
    /// <remarks>
    /// The following properties are required:
    /// <list type="bullet">
    /// <item><description>LicensePlate</description></item>
    /// <item><description>LicenseState</description></item>
    /// </list>
    /// </remarks>
    public class LookupByLicensePlateRequestModel : BaseSdkRequest
    {
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string LicenseState { get; set; }
    }
}