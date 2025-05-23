namespace BelronUS.VehicleSDKs.V2.Utilities
{
    public static class ExternalEndpoints
    {
        public static class VehicleApi
        {
            public const string GetLookupByAddress = "/vehicle/api/v2/vehicle/lookup/address";
            public const string GetLookupByCarId = "/vehicle/api/v2/vehicle/lookup/id";
            public const string GetLookupByVin = "/vehicle/api/v2/vehicle/lookup/vin";
            public const string GetLookupByCarProperties = "/vehicle/api/v2/vehicle/lookup";
            public const string GetLookupByLicensePlate = "/vehicle/api/v2/vehicle/lookup/plate";
            public const string GetRegistrationLookupPermissable = "/vehicle/api/v2/vehicle/lookup/registration-lookup-permissible";
            public const string GetSearchVehicle = "/vehicle/api/v2/vehicle/search";
        }
    }
}