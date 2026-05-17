using Zonit.Extensions;

namespace Extensions.MudBlazor.Models;

/// <summary>
/// View model for the MudBlazor + Value Objects sample form. Field types are Zonit
/// VOs — <see cref="Title"/>, <see cref="UrlSlug"/>, <see cref="Url"/>,
/// <see cref="Description"/>. <c>ZonitTextField&lt;T&gt;</c> infers <c>T</c> from
/// <c>@bind-Value</c>, so the consumer Razor never spells the generic out.
/// </summary>
public sealed class FormModel
{
    public Title       Title       { get; set; } = "Hello Zonit";
    public UrlSlug     Slug        { get; set; } = "hello-zonit";
    public Url         HomeUrl     { get; set; } = "https://zonit.dev";
    public Description Description { get; set; } = "Type something and watch the VO validate live.";
}
