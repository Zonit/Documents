using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Documents.Website.Pages;

[Route(Route)]
public sealed partial class Home : PageBase
{
    /// <summary>
    /// Public route of the landing page. Referenced by <see cref="WebsiteArea.Navigation"/>
    /// so renaming the path here propagates to the sidebar automatically. Class is named
    /// <c>Home</c> rather than <c>Index</c> to avoid the BCL <c>System.Index</c> clash.
    /// </summary>
    public const string Route = "/";

    private sealed record Feature(string Title, string Summary);

    private readonly Feature[] _features =
    [
        new("Cultures",      "Per-circuit language + time zone switching, with translation cache and missing-key tracking."),
        new("Auth",          "Identity value object, RequirePermission / RequireRole attributes, wildcard policies."),
        new("Workspace",     "Organizations + Projects with reactive providers and IOrganizationSource hooks."),
        new("Tenants",       "Per-domain Site / Theme / Maintenance / SocialMedia settings, solo or multi-tenant."),
        new("Components",    "PageViewBase, PageEditBase, AutoSave, toasts, breadcrumbs, cookies."),
        new("Value objects", "Title, Url, Money, Price, Identity, TimeZone, Permission — AOT-safe, JSON-serializable."),
    ];
}
