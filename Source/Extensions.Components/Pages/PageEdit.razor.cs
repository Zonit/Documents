using Extensions.Components.Models;
using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.Components.Pages;

[Route(Route)]
public sealed partial class PageEdit : PageEditBase<TaskViewModel>
{
    public const string Route = "/components/page-edit";

    protected override async Task SubmitAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(400, cancellationToken); // pretend to call a backend
        Toast.AddSuccess("Saved \"{0}\"", Model.Title);
    }

    private void Reset() => ResetModel();
}
