using BelronUS.VehicleSDKs.V2.Models.Request;
using BelronUS.VehicleSDKs.V2.Models.Response;

namespace BelronUS.VehicleSDKs.V2;

public interface IVehicleSdk
{
    Task<VehicleResponseModel> LookupByCarId(LookupByCarIdRequestModel request);
    Task<IEnumerable<VehicleResponseModel>> LookupByAddress(LookupByAddressRequestModel request);
}
