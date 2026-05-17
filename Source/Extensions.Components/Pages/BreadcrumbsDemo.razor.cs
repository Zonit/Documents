using Microsoft.AspNetCore.Components;
using Zonit.Extensions;
using Zonit.Extensions.Website;

namespace Extensions.Components.Pages;

[Route(Route)]
public sealed partial class BreadcrumbsDemo : PageBase
{
    public const string Route = "/components/breadcrumbs";

    protected override bool? ShowBreadcrumbs => true;

    protected override List<BreadcrumbsModel> Breadcrumbs =>
    [
        new() { Text = "Home",        Href = "/" },
        new() { Text = "Components",  Href = PageView.Route },
        new() { Text = "Breadcrumbs" },
    ];
}
