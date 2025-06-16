using VehicleCore.DomainModel.BaseModels;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.DomainModel.Models;

public class Motorcycle:TrackableEntity
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string TrackingCode { get; set; } = string.Empty;
    public FuelType Fuel { get; set; }

    public static Motorcycle Create(string title,FuelType fuel)
    {
        return new Motorcycle(title,fuel);
    }
    public Motorcycle(string title, FuelType fuel)
    {
        Title = title;
        IsActive = true;
        Fuel = fuel;
    }

    public void Update(string title, FuelType fuel)
    {
        Title = title;
        Fuel = fuel;
    }

    public void ToggleActivation()
    {
        IsActive = !IsActive;
    }




}
