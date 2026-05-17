using Documents.Docs.Pages;
using Zonit.Extensions.Website;

namespace Documents.Docs;

/// <summary>
/// Root documentation area. Hosts the documentation home page on <c>/docs</c>
/// (mounted via the Zonit.Dashboard host) and contributes the navigation entry
/// that anchors every other feature-area sidebar group.
/// </summary>
public sealed class DocsArea : IWebsiteArea
{
    public string Key => "docs";

    public IReadOnlyList<NavGroup> Navigation { get; } =
    [
        new NavGroup
        {
            Title = "Documentation",
            Order = 0,
            Children =
            [
                new NavItem { Title = "Overview", Url = Home.Route, Match = true },
            ],
        },
    ];
}
