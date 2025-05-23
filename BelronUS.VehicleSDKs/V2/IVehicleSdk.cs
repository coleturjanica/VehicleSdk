using BelronUS.VehicleSDKs.V2.Models.Request;
using BelronUS.VehicleSDKs.V2.Models.Response;
using BelronUS.SDK.Base;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BelronUS.VehicleSDKs.V2
{
    public interface IVehicleSdk
    {
        T CreateRequest<T>(Action<T> initializer) where T : BaseSdkRequest, new();
        Task<VehicleResponseModel> LookupByCarId(LookupByCarIdRequestModel request);
        Task<IEnumerable<VehicleResponseModel>> LookupByAddress(LookupByAddressRequestModel request);
        Task<VehicleResponseModel> LookupByVin(LookupByVinRequestModel request);
        Task<IEnumerable<VehicleResponseModel>> LookupByCarProperties(LookupByCarPropertiesRequestModel request);
        Task<VehicleResponseModel> LookupByLicensePlate(LookupByLicensePlateRequestModel request);
        Task<RegistrationLookupPermissableResponseModel> GetRegistrationLookupPermissable(RegistrationLookupPermissableRequestModel request);
        Task<IEnumerable<VehicleResponseModel>> SearchVehicle(SearchVehicleRequestModel request);
    }
}
