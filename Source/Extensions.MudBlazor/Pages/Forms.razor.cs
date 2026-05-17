using Extensions.MudBlazor.Models;
using Microsoft.AspNetCore.Components;
using Zonit.Extensions.Website;

namespace Extensions.MudBlazor.Pages;

[Route(Route)]
public sealed partial class Forms : PageEditBase<FormModel>
{
    public const string Route = "/mudblazor/forms";

    /// <summary>
    /// Provides the initial model for the form. Returning a fresh <see cref="FormModel"/>
    /// here keeps the page demonstrating <em>PageViewBase.LoadAsync</em> + <em>PageEditBase</em>
    /// composition: a real backend call would replace the <c>new()</c> below.
    /// </summary>
    protected override Task<FormModel?> LoadAsync(CancellationToken cancellationToken)
        => Task.FromResult<FormModel?>(new FormModel());

    /// <summary>
    /// Pretends to call a backend. <c>HasChanges</c> resets to <c>false</c>
    /// automatically after a successful submit.
    /// </summary>
    protected override async Task SubmitAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        Toast.AddSuccess("Saved \"{0}\"", Model.Title.Value);
    }
}
