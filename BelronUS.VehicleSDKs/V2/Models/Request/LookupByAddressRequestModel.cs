using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request
{

    /// <summary>
    /// Represents a request model used to search for a vehicle by its address.
    /// </summary>
    /// <remarks>
    /// The following properties are required:
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
}