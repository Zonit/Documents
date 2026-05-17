using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.ValueObjects.Pages;

[Route(Route)]
public sealed partial class Time : PageBase
{
    public const string Route = "/vo/time";

    private string _time = "09:00";
}
