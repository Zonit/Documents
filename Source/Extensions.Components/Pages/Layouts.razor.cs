using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.Components.Pages;

[Route(Route)]
public sealed partial class Layouts : PageBase
{
    public const string Route = "/components/layouts";

    private string CurrentState => LayoutKey switch
    {
        null when LayoutContext.IsNoLayout => "(none — render raw)",
        null => "(no override — using static / default)",
        ""   => "(empty — Site default)",
        var k => $"\"{k}\"",
    };

    private void UseBox()  => LayoutKey = "Demo.Box";
    private void UseNone() => LayoutKey = null;
    private void Reset()   => LayoutContext.ClearOverride();
}
