using System.Collections.Generic;
using BelronUS.SDK.Base;

namespace BelronUS.VehicleSDKs.V2.Models.Response
{
    public class VehicleResponseModel : BaseSdkResponse
    {
        public string CarId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string BodyStyle { get; set; }
        public string VehicleCategory { get; set; }
        public string PreferredAdasTool { get; set; }
        public bool IsProblemVehicle { get; set; }
        public string ImageUrl { get; set; }
        public string SpecialVehicleCategory { get; set; }
        public string SpecialVehicleClass { get; set; }
        public bool IsSpecialVehicle { get; set; }
        public bool CanSafeliteService { get; set; }
        public bool HasSplitWindshield { get; set; }
        public List<string> RecalibrationServices { get; set; }
        public bool IsMobileStaticRecalibrationApplicable { get; set; }
        public string VehicleSubType { get; set; }
        public bool IsAdasVehicle { get; set; }
        public bool IsBigTruck { get; set; }
    }
}
