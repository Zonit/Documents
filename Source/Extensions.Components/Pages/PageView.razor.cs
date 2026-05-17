using Extensions.Components.Models;
using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.Components.Pages;

[Route(Route)]
public sealed partial class PageView : PageViewBase<TaskViewModel>
{
    public const string Route = "/components/page-view";

    // Simulate I/O delay to make IsLoading observable.
    protected override async Task<TaskViewModel?> LoadAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(250, cancellationToken);
        return new TaskViewModel();
    }

    private Task Reload() => RefreshAsync();
}
