using System.ComponentModel;
using VehicleCore.DomainModel.Attributes;

namespace VehicleCore.DomainModel.Enums;

[EnumEndpoint("/GearBoxTypes")]
public enum GearBoxType
{
    [Description("دنده ای")]
    Manual=1,
    [Description("اتوماتیک")]
    Automatic=2

}
