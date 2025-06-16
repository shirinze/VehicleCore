using System.ComponentModel;
using VehicleCore.DomainModel.Attributes;

namespace VehicleCore.DomainModel.Enums;

[EnumEndpoint("/FuelTypes")]
public enum FuelType
{
    [Description("بنزینی")]
    Gas = 1,
    [Description("برقی")]
    Electronic = 2
}
