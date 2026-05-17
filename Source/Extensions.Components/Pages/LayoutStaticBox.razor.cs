using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.Components.Pages;

[Route(Route)]
[LayoutKey("Demo.Box")]
public sealed partial class LayoutStaticBox : PageBase
{
    public const string Route = "/components/layouts/static-box";
}
