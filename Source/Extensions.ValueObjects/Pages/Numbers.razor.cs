using Microsoft.AspNetCore.Components;
using Zonit.Extensions;
using Zonit.Extensions.Website;

namespace Extensions.ValueObjects.Pages;

[Route(Route)]
public sealed partial class Numbers : PageBase
{
    public const string Route = "/vo/numbers";

    private string _input         = "1.234,56";
    private string _color         = "#2563EB";
    private string _currencyInput = "USD";

    private Money? _money => Money.TryParse(_input, null, out var v) ? v : null;
    private Price? _price => Price.TryParse(_input, null, out var v) ? v : null;
}
