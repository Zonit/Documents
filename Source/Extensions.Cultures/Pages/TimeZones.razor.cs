using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Cultures;
using Zonit.Extensions.Website;

namespace Extensions.Cultures.Pages;

[Route(Route)]
public sealed partial class TimeZones : PageBase
{
    /// <summary>
    /// Public route of the time-zone page. Referenced from
    /// <see cref="CulturesArea.Navigation"/>.
    /// </summary>
    public const string Route = "/cultures/time-zone";

    [Inject] private ICultureManager CultureManager { get; set; } = default!;

    private void OnZoneChanged(ChangeEventArgs e)
    {
        if (e.Value is string id)
            CultureManager.SetTimeZone(id);
    }
}
