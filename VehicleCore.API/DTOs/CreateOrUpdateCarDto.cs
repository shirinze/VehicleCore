using VehicleCore.DomainModel.Enums;

namespace VehicleCore.API.DTOs;

public class CreateOrUpdateCarDto
{
    public required string Title { get; set; }
    public required GearBoxType GearBox { get; set; }

}
