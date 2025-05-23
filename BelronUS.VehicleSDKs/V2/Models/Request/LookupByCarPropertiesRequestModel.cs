using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request
{

    /// <summary>
    /// Represents a request model used to search for a vehicle by its car properties.
    /// </summary>
    /// <remarks>
    /// The following properties are required:
    /// <list type="bullet">
    /// <item><description>Year</description></item>
    /// <item><description>Make</description></item>
    /// <item><description>Model</description></item>
    /// </list>
    /// </remarks>
    public class LookupByCarPropertiesRequestModel : BaseSdkRequest
    {
        [Required]
        public int Year { get; set; }
        
        [Required]
        public string Make { get; set; }
        
        [Required]
        public string Model { get; set; }
        
        public string BodyStyle { get; set; }
    }
}