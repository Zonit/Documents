using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Cultures;
using Zonit.Extensions.Website;

namespace Extensions.Cultures.Pages;

[Route(Route)]
public sealed partial class CulturesIndex : PageBase
{
    /// <summary>
    /// Public route of the Cultures overview. Referenced by
    /// <see cref="CulturesArea.Navigation"/>; rename here and the sidebar follows.
    /// </summary>
    public const string Route = "/cultures";

    [Inject] private ICultureManager CultureManager { get; set; } = default!;

    private void OnCultureChanged(ChangeEventArgs e)
    {
        if (e.Value is string code)
            CultureManager.SetCulture(code);
    }
}
