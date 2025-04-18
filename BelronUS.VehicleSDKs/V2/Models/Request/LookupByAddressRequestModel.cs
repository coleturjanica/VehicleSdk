using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Request;

public class LookupByAddressRequestModel : BaseSdkRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string RegisteredStreetAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Zip { get; set; }
}