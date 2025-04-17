using System.Net.Http.Json;
using System.Text.Json;
using BelronUS.Http.HttpClientHelper;
using BelronUS.Http.HttpClientHelper.Interface;
using BelronUS.ServiceHelpers.BelronUSJsonSerializerOptions;
using Microsoft.Extensions.Logging;
using VehicleSdkV2.Models.Response;
using VehicleSdkV2.Utilities;

namespace VehicleSdkV2;

public interface IVehicleSdkV2Here
{
    void printHi();
    Task<VehicleResponseModel> GetVehicleByCarId(string carId);
}
