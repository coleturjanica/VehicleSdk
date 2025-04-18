using BelronUS.VehicleSDK.V2.Models.Response;

namespace BelronUS.VehicleSDK.V2;

public interface IVehicleSdkV2
{
    void printHi();
    Task<VehicleResponseModel> GetVehicleByCarId(string carId);
}
