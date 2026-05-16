using Example;
using Example.Components;
using Zonit.Extensions;
using Zonit.Extensions.Cultures.Options;
using Zonit.Extensions.Website;

var builder = WebApplication.CreateBuilder(args);

// ─── Build-time: register the Website framework + each Area's DI services.
// Areas implementing IWebsiteServices (DemoStore, IAuthSource, IOrganizationSource,
// IProjectSource, ITenantSource registrations) flow through here exactly once,
// regardless of how many Sites later mount them.
builder.Services.AddWebsite(opts =>
{
    // MemoryCache / RazorComponents are services-only flags and default to true.
    // Set opts.Controllers = true if your areas need [ApiController] endpoints.
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

// ─── Zonit.Dashboard 10.0.0-preview1 — services-time wiring (idempotent).
// Registers IThemeManager, IExtensionRegistry, IDashboardCurrentSite, all built-in
// themes, the theme-selector drawer extension, and the layout-by-key bindings
// ("Dashboard.Main" / "Zonit.Minimal").
builder.Services.AddDashboard();

// ─── Cultures: supported list drives the language switcher and Accept-Language fallback.
builder.Services.Configure<CultureOption>(o =>
{
    o.DefaultCulture = "en-US";
    o.DefaultTimeZone = "Europe/Warsaw";
    o.SupportedCultures = ["en-US", "pl-PL", "de-DE", "fr-FR"];
});

var app = builder.Build();

// ─── Runtime: mount each Site. Single Site at root in this demo.
//
// Multi-Site example:
//
//   app.UseWebsite<App>("/admin", o =>
//   {
//       o.Permission = "admin";          // gate every page under /admin behind the policy
//       o.AddArea<Example.Auth.AuthArea>();      // login also at /admin/login
//       o.AddArea<MyAdminArea>();
//   });
//
// Each UseWebsite<App>(…) call creates an isolated MapWhen branch with its own
// PathBase + MapRazorComponents — declare the catch-all root Site LAST.

// ─── Zonit.Dashboard mount under /admin — register BEFORE the root site so its
// MapWhen branch wins for /admin/* URLs (the root site at "/" is a catch-all and
// would otherwise swallow them).
//
// UseDashboard is signature-compatible with UseWebsite<TApp> but the TApp generic
// is gone — the dashboard ships its own root component (DashboardApp). Layout
// knobs / extension whitelists / SiteOptions mirrors all live on DashboardSiteOptions.
app.UseDashboard("/admin", o =>
{
    // Same dev-friendly overrides as the root site below.
    o.Compression = !builder.Environment.IsDevelopment();
    o.HttpsRedirection = !builder.Environment.IsDevelopment();

    // Mount the same content areas under /admin so the dashboard drawer has
    // navigation entries to show. Real consumers would mount a dedicated set of
    // admin-only areas here instead.
    o.AddArea<ComponentsArea>();
    o.AddArea<MudBlazorArea>();
    o.AddArea<ValueObjectsArea>();

    // Demo overrides of the per-mount layout knobs.
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
