using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.Cultures.Pages;

[Route(Route)]
public sealed partial class Translation : PageBase
{
    /// <summary>
    /// Public route of the translation page. Referenced from
    /// <see cref="CulturesArea.Navigation"/>.
    /// </summary>
    public const string Route = "/cultures/translation";

    private string _input = "Hello there";
}
