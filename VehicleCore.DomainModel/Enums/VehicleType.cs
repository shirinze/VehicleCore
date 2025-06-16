using System.ComponentModel;
using VehicleCore.DomainModel.Attributes;

namespace VehicleCore.DomainModel.Enums;

[EnumEndpoint("/VehicleTypes")]
public enum VehicleType
{
    [Info("Priority",1)]
    [Info("HasActiveSales",true)]
    [Description("اتومبیل")]
    Car=1,

    [Info("Priority", 2)]
    [Info("HasActiveSales", true)]
    [Description("موتورسیکلت")]
    MotorCycle =2
}
