using VehicleSdkV2.Models.Response;

namespace VehicleSdkV2;

public class Methods
{
    public static void printHi() {
        Console.WriteLine("hi");
    }

    public VehicleResponseModel LookupByCarId (string carId) {
        return new VehicleResponseModel();
    }
}
