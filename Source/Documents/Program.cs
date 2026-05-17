using Documents;
using Documents.Components;
using Zonit.Extensions;
using Zonit.Extensions.Cultures.Options;
using Zonit.Extensions.Website;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebsite(opts =>
{
    opts.AddArea<HomeArea>();
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
app.UseDashboard("/dashboard", o =>
{
    o.Compression = !builder.Environment.IsDevelopment();
    o.HttpsRedirection = !builder.Environment.IsDevelopment();

    o.AddArea<ComponentsArea>();
    o.AddArea<MudBlazorArea>();
    o.AddArea<ValueObjectsArea>();

    o.Layout.LeftDrawerWidth = 260;
    o.Layout.ShowBreadcrumbs = true;
});

app.UseWebsite<App>("/", o =>
{
    o.Mode = WebsiteMode.Server;
    // Browser-Link / dotnet-watch can't inject scripts into a Brotli-compressed body.
    o.Compression = !builder.Environment.IsDevelopment();
    // dev demo runs HTTP-only on a random port — opt out of HTTPS redirect.
    o.HttpsRedirection = !builder.Environment.IsDevelopment();

    o.AddArea<HomeArea>();
    o.AddArea<CulturesArea>();
    o.AddArea<AuthArea>();
    o.AddArea<OrganizationsArea>();
    o.AddArea<ProjectsArea>();
    o.AddArea<TenantsArea>();
    o.AddArea<ComponentsArea>();
    o.AddArea<ValueObjectsArea>();
    o.AddArea<MudBlazorArea>();
});

app.Run();
