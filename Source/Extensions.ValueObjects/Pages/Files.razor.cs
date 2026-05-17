using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Zonit.Extensions;
using Zonit.Extensions.Website;

namespace Extensions.ValueObjects.Pages;

[Route(Route)]
public sealed partial class Files : PageBase
{
    public const string Route = "/vo/files";

    private Asset _asset = Asset.Empty;
    private string? _error;
    private string _sizeInput = "1.5 GB";
    private string _color = "#2563EB";

    private async Task OnFile(InputFileChangeEventArgs e)
    {
        _error = null;
        try
        {
            var file = e.File;

            // Cap at 10 MB so a misclick doesn't try to slurp a multi-gig blob into memory.
            // The backing Asset.MaxSize is 100 MB — this is a UI-side guard for the demo only.
            await using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            _asset = new Asset(ms.ToArray(), file.Name);
        }
        catch (Exception ex)
        {
            _asset = Asset.Empty;
            _error = $"Could not load file: {ex.Message}";
        }
    }
}
