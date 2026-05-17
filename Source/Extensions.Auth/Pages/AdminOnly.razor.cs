using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;
using Zonit.Extensions.Website.Authentication;

namespace Extensions.Auth.Pages;

[Route(Route)]
[RequirePermission("settings.write")]
public sealed partial class AdminOnly : PageBase
{
    public const string Route = "/auth/admin-only";
}
