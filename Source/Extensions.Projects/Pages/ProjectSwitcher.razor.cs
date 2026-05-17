using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Projects;
using Zonit.Extensions.Website;

namespace Extensions.Projects.Pages;

[Route(Route)]
public sealed partial class ProjectSwitcher : PageBase
{
    public const string Route = "/projects/switcher";

    [Inject] private ICatalogManager Manager { get; set; } = default!;

    private async Task SwitchTo(Guid id)
    {
        await Manager.SwitchProjectAsync(id);
        Toast.AddSuccess("Switched to {0}", Catalog.Project.Name.Value);
    }
}
