using System.ComponentModel.DataAnnotations;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request;

public class LookupByCarIdRequestModel : BaseSdkRequest
{
    [Required]
    public string CarId { get; set; }
}
