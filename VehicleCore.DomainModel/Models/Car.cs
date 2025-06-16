using VehicleCore.DomainModel.BaseModels;
using VehicleCore.DomainModel.Enums;

namespace VehicleCore.DomainModel.Models;

public class Car:TrackableEntity
{
    

    public string Title { get; private set; } = string.Empty;
    public bool IsActive { get;private set; }
    public string TrackingCode { get; private set; } = string.Empty;
    public GearBoxType GearBox { get;private set; }

    public static Car Create(string title,string trackingCode,GearBoxType gearBox)
    {
        return new Car(title,trackingCode,gearBox);
    }
    public Car(string title, string trackingCode, GearBoxType gearBox)
    {
        Title = title;
        IsActive = true;
        TrackingCode = trackingCode;
        GearBox= gearBox;
    }

    public void Update(string title,GearBoxType gearBox)
    {
        Title = title;
        GearBox = gearBox;
    }
    public void ToggleActivation()
    {
        IsActive = !IsActive;
    }
}
