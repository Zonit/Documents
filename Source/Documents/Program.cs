using Documents;
using Documents.Components;
using Documents.Docs;
using Documents.Website;
using Zonit.Extensions;
using Zonit.Extensions.Cultures.Options;
using Zonit.Extensions.Website;

var builder = WebApplication.CreateBuilder(args);

// AddWebsite registers framework services AND every IWebsiteArea so each area's
// ConfigureServices runs against the DI container before app.Build(). The per-mount
// AddArea<T>() calls below only *activate* a registered area on a specific mount.
builder.Services.AddWebsite(opts =>
{
    opts.AddArea<WebsiteArea>();
    opts.AddArea<DocsArea>();
    opts.AddArea<CulturesArea>();
    opts.AddArea<AuthArea>();
    opts.AddArea<OrganizationsArea>();
    opts.AddArea<ProjectsArea>();
    opts.AddArea<TenantsArea>();
    opts.AddArea<ComponentsArea>();
    opts.AddArea<ValueObjectsArea>();
    opts.AddArea<MudBlazorArea>();
});

builder.Services.AddDashboard();

builder.Services.Configure<CultureOption>(o =>
{
    o.DefaultCulture = "en-US";
    o.DefaultTimeZone = "Europe/Warsaw";
    o.SupportedCultures = ["en-US", "pl-PL", "de-DE", "fr-FR"];
});

var app = builder.Build();

// IMPORTANT: declare every non-root mount BEFORE the root mount. The root mount
// (Directory == "/") finishes with a terminal UseEndpoints middleware in the main
// pipeline; every app.MapWhen branch registered AFTER it would be unreachable. The
// framework now fails fast with an InvalidOperationException when this order is
// violated.
// /docs — full documentation site. The Zonit.Dashboard mount brings MudBlazor chrome
// (theme + providers) so every doc page can use MudBlazor markup directly. All
// feature-area projects ship pages under their own URL prefix (e.g. /cultures, /auth);
// when mounted here they automatically become /docs/cultures, /docs/auth, ... via the
// dashboard's UsePathBase.
app.UseDashboard("/docs", o =>
{
    o.Compression = !builder.Environment.IsDevelopment();
    o.HttpsRedirection = !builder.Environment.IsDevelopment();

    o.AddArea<DocsArea>();
    o.AddArea<CulturesArea>();
    o.AddArea<AuthArea>();
    o.AddArea<OrganizationsArea>();
    o.AddArea<ProjectsArea>();
    o.AddArea<TenantsArea>();
    o.AddArea<ComponentsArea>();
    o.AddArea<ValueObjectsArea>();
    o.AddArea<MudBlazorArea>();

    o.Layout.LeftDrawerWidth = 260;
    o.Layout.ShowBreadcrumbs = true;
});

// / — minimal public landing. Carries only the WebsiteArea (single marketing page
// that links into /docs). Heavy demos do NOT live here so the root mount stays small.
app.UseWebsite<App>("/", o =>
{
    o.Mode = WebsiteMode.Server;
    // Browser-Link / dotnet-watch can't inject scripts into a Brotli-compressed body.
    o.Compression = !builder.Environment.IsDevelopment();
    // dev demo runs HTTP-only on a random port — opt out of HTTPS redirect.
    o.HttpsRedirection = !builder.Environment.IsDevelopment();

    o.AddArea<WebsiteArea>();
});

app.Run();
