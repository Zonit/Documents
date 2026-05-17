using Microsoft.AspNetCore.Components;
using MudBlazor;
using Zonit.Extensions.Website;

namespace Documents.Docs.Pages;

[Route("/"), Route(Route)]
public sealed partial class Home : PageBase
{
    /// <summary>
    /// Route of the docs landing page, relative to the <c>/docs</c> mount — runtime URL
    /// is <c>/docs/home</c>. Referenced by <see cref="DocsArea.Navigation"/>.
    /// </summary>
    public const string Route = "/home";

    private sealed record Card(string Title, string Summary, string Icon, string Url);

    private readonly Card[] _cards =
    [
        new("Cultures",       "Translation provider, time-zone conversion, language switching.",      Icons.Material.Filled.Translate,  "/docs/cultures"),
        new("Auth",           "Identity VO, RequirePermission, RequireRole, AuthorizeView.",          Icons.Material.Filled.Security,   "/docs/auth"),
        new("Organizations",  "Workspace switcher, IOrganizationSource, reactive providers.",         Icons.Material.Filled.Workspaces, "/docs/workspace"),
        new("Projects",       "Catalog switcher, IProjectSource, per-workspace filtering.",           Icons.Material.Filled.Inventory2, "/docs/catalog"),
        new("Tenants",        "Per-domain Site / Theme / Maintenance / SocialMedia options.",         Icons.Material.Filled.Domain,     "/docs/tenants"),
        new("Components",     "PageViewBase, PageEditBase, AutoSave, toasts, breadcrumbs, cookies.",  Icons.Material.Filled.Widgets,    "/docs/components"),
        new("Value objects",  "Title, Url, Money, Price, Identity, TimeZone, Permission, Currency.",  Icons.Material.Filled.DataObject, "/docs/value-objects"),
        new("MudBlazor bridge","ZonitTextField / ZonitTextArea — Value-Object-aware Mud inputs.",     Icons.Material.Filled.Palette,    "/docs/mudblazor/forms"),
    ];
}
