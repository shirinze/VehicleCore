namespace VehicleCore.DomainService;

public class Settings
{
    public required TrackingCodeSetting TrackingCode { get; set; }
}
public class TrackingCodeSetting
{
    public required string BaseURL { get; set; }
    public required string GetUrl { get; set; }
    public required string Prefix { get; set; }
}