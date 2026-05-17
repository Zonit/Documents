using Extensions.Cultures.Pages;
using Zonit.Extensions.Website;

namespace Documents;

/// <summary>
/// Demo area exercising <see cref="Zonit.Extensions.Cultures.ICultureProvider"/>
/// / <see cref="Zonit.Extensions.Cultures.ICultureManager"/> — translation, time-zone
/// conversion, language switching.
/// </summary>
public sealed class CulturesArea : IWebsiteArea
{
    public string Key => "cultures";

    public IReadOnlyList<NavGroup> Navigation { get; } =
    [
        new NavGroup
        {
            Title = "Cultures",
            Order = 10,
            Children =
            [
                new NavItem { Title = "Index",       Url = CulturesIndex.Route },
                new NavItem { Title = "Translation", Url = Translation.Route   },
                new NavItem { Title = "Time zone",   Url = TimeZones.Route     },
            ],
        },
    ];
}
