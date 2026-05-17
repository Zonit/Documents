using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.ValueObjects.Pages;

[Route(Route)]
public sealed partial class Strings : PageBase
{
    public const string Route = "/vo/strings";

    private string _titleInput       = "Hello Zonit";
    private string _urlInput         = "https://zonit.dev";
    private string _slugInput        = "Hello World!";
    private string _pathInput        = "/orders?id=42";
    private string _descriptionInput = "A short tagline that fits in a card subtitle.";
    private string _contentInput     = "<p>Long-form <strong>HTML</strong> goes here.</p>";
    private string _credInput        = "jan@example.com";
}
