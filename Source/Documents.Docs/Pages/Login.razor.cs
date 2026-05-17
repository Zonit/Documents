using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Documents.Docs.Pages;

/// <summary>
/// SSR login form for the Docs demo. Lives under the <c>/docs</c> mount so its
/// relative POST <c>auth/login</c> hits <see cref="Extensions.Auth.AuthArea.MapEndpoints"/>
/// which is registered on the same branch.
/// </summary>
public sealed partial class Login : PageBase
{
    public const string Route = "/login";

    [SupplyParameterFromQuery]
    public string? Error { get; set; }

    [SupplyParameterFromQuery(Name = "returnUrl")]
    public string ReturnUrl { get; set; } = "/";
}
