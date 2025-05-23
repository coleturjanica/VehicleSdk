
using System;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Response
{
    public class RegistrationLookupPermissableResponseModel : BaseSdkResponse
    {
        public string ServiceType { get; set; }
        public string BaseValueType { get; set; }
        public string BaseValue { get; set; }
        public bool Permissable { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}