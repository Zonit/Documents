using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;
using Zonit.Extensions.Website.Authentication;

namespace Extensions.Auth.Pages;

[Route(Route)]
[RequirePermission("users.read")]
public sealed partial class RequirePermissionDemo : PageBase
{
    public const string Route = "/auth/require-permission";
}
