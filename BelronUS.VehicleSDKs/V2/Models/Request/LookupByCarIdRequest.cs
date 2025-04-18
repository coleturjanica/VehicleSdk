using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request;

public class LookupByCarIdRequest : BaseSdkRequest
{
    public string CarId { get; set; }
}
