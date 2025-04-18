using BelronUS.VehicleSDKs.V2.Models.Response;

namespace BelronUS.VehicleSDKs.V2;

public interface IVehicleSdk
{
    void printHi();
    Task<VehicleResponseModel> LookupByCarId(string carId);
}
