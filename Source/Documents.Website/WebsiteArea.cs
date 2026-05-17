using Documents.Website.Pages;
using Zonit.Extensions.Website;

namespace Documents.Website;

/// <summary>
/// Public-facing landing area mounted on <c>/</c>. Carries a single marketing/redirect
/// page; the documentation itself lives under <c>/docs</c> (see
/// <c>Documents.Docs.DocsArea</c>).
/// </summary>
public sealed class WebsiteArea : IWebsiteArea
{
    public string Key => "website";

    public IReadOnlyList<NavGroup> Navigation { get; } =
    [
        new NavGroup
        {
            Title = "Zonit",
            Order = 0,
            Children =
            [
                new NavItem { Title = "Home",          Url = Home.Route,  Match = true  },
                new NavItem { Title = "Documentation", Url = "/docs",     Match = false },
            ],
        },
    ];
}
