using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request
{
    /// <summary>
    /// Represents a request model used to search for a vehicle by its CarId.
    /// </summary>
    /// <remarks>
    /// The following properties are required:
    /// <list type="bullet">
    /// <item><description>CarId</description></item>
    /// </list>
    /// </remarks>
    public class LookupByCarIdRequestModel : BaseSdkRequest
    {
        [Required]
        public string CarId { get; set; }
    }
}
