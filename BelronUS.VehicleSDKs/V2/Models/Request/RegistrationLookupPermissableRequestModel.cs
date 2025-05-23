
using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request
{
    /// <summary>
    /// Represents a request model used to search for a vehicle to get its permissible use maintenance data.
    /// </summary>
    /// <remarks>
    /// The following properties are required:
    /// <list type="bullet">
    /// <item><description>ServiceType</description></item>
    /// <item><description>BaseValueType</description></item>
    /// <item><description>BaseValue</description></item>
    /// </list>
    /// </remarks>
    public class RegistrationLookupPermissableRequestModel : BaseSdkRequest
    {
        [Required]
        public string ServiceType { get; set; }
        [Required]
        public string BaseValueType { get; set; }
        [Required]
        public string BaseValue { get; set; }
    }
}