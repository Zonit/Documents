using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.Components.Pages;

[Route(Route)]
[NoLayout]
public sealed partial class LayoutNoLayout : PageBase
{
    public const string Route = "/components/layouts/no-layout";
}
