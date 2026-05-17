using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.Components.Pages;

[Route(Route)]
public sealed partial class Cookies : PageBase
{
    public const string Route = "/components/cookies";

    private const string Key = "example.demo";
    private string _value = "hello";
    private string? _loaded;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Cookie.RefreshAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task Save()
    {
        await Cookie.SetAsync(Key, _value, TimeSpan.FromDays(1));
        await Cookie.RefreshAsync();
        Toast.AddSuccess("Saved cookie {0}", Key);
    }

    private void Load()
    {
        _loaded = Cookie.Get(Key)?.Value ?? "(not set)";
        if (Cookie.Get(Key) is null) Toast.AddWarning("Cookie {0} is not set", Key);
    }

    private async Task Delete()
    {
        await Cookie.SetAsync(Key, string.Empty, DateTime.UtcNow.AddDays(-1));
        await Cookie.RefreshAsync();
        _loaded = null;
        Toast.AddInfo("Deleted {0}", Key);
    }

    private async Task Refresh()
    {
        await Cookie.RefreshAsync();
        Toast.AddInfo("Refreshed cookie snapshot");
    }
}
