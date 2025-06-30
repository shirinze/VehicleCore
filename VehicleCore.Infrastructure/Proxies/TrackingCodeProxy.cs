
using Microsoft.Extensions.Options;
using VehicleCore.DomainService;
using VehicleCore.DomainService.Proxies;

namespace VehicleCore.Infrastructure.Proxies;

public class TrackingCodeProxy(IOptions<Settings> setting,HttpClient httpClient) : ITrackingCodeProxy
{
    private readonly TrackingCodeSetting _setting = setting.Value.TrackingCode;
    public async Task<string> Get(CancellationToken cancellationToken)
    {
        var url = string.Format(_setting.GetUrl, _setting.Prefix);
        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Tracking code is not available");
        }
        response.EnsureSuccessStatusCode();
        var trackingCode =await response.Content.ReadAsStringAsync(cancellationToken);
        return trackingCode;
    }
}
