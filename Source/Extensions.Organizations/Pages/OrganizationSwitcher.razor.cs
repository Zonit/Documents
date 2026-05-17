using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Organizations;
using Zonit.Extensions.Website;

namespace Extensions.Organizations.Pages;

[Route(Route)]
public sealed partial class OrganizationSwitcher : PageBase
{
    public const string Route = "/organizations/switcher";

    [Inject] private IWorkspaceManager Manager { get; set; } = default!;

    private async Task SwitchTo(Guid id)
    {
        await Manager.SwitchOrganizationAsync(id);
        Toast.AddSuccess("Switched to {0}", Workspace.Organization.Name.Value);
    }
}
