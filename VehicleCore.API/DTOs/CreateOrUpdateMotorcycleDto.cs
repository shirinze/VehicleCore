using VehicleCore.DomainModel.Enums;

namespace VehicleCore.API.DTOs;

public class CreateOrUpdateMotorcycleDto
{
    public required string Title { get; set; }
    public required FuelType Fuel { get; set; }

}
