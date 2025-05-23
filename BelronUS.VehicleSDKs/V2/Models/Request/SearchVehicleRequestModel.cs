using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request
{
    /// <summary>
    /// Represents a request model used to search for a vehicle by seemingly any value like make, model, year, etc.
    /// </summary>
    /// <remarks>
    /// The following properties are required:
    /// <list type="bullet">
    /// <item><description>SearchValue</description></item>
    /// </list>
    /// </remarks>
    public class SearchVehicleRequestModel : BaseSdkRequest
    {
        [Required]
        public string SearchValue { get; set; }
    }
}