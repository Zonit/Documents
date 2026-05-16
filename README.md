# Zonit.Documents

Live, runnable documentation for the entire **Zonit** stack. Every public
package gets its own demo area inside the host app under `Source/Example/`,
so opening any page in the running site doubles as the page's spec, screenshot,
and integration test.

The same host also mounts **Zonit.Dashboard** at `/admin`, so the dashboard's
themes, drawer extensions, navigation aggregation, and per-mount options are
testable end-to-end in one process.

## Repository layout

```
Zonit.Documents/
├── Directory.Build.props        # Sets IsZonitSdkWorkspace=true (matches Zonit.Sdk).
├── Zonit.Documents.sln
├── Docs/                        # Long-form Markdown (concepts, ADRs, design notes).
├── Source/
│   └── Example/                 # All Example.* demo areas + the host (Example/).
│       ├── Example/             # ASP.NET Core host (Program.cs, App.razor, Routes.razor).
│       ├── Example.Auth/        # IAuthSource demo — login, logout, gated pages.
│       ├── Example.Components/  # Zonit.Extensions.Website ZonitText* + layout slots.
│       ├── Example.Cultures/    # ICultureProvider + language switcher.
│       ├── Example.MudBlazor/   # MudBlazor adapter components.
│       ├── Example.Organizations/  # IOrganizationSource demo.
│       ├── Example.Projects/    # IProjectSource demo.
│       ├── Example.Shared/      # Helpers reused by multiple demo areas.
│       ├── Example.Tenants/     # ITenantSource demo (multi-tenant routing).
│       └── Example.ValueObjects/   # Title / UrlPath / FileSize / Permission / Color demos.
└── external/                    # Git submodules — Zonit.* source repos.
    ├── Zonit.Extensions/
    └── Zonit.Services.Dashboard/
```

## First-time setup

The `Example.*` projects reference Zonit packages through `<ProjectReference>`
paths that point into `external/`. Those folders are git submodules — they
have to be initialised before the solution builds.

```pwsh
# 1. Clone with submodules. If you've already cloned, run the second line.
git clone --recurse-submodules https://github.com/Zonit/Documents.git
# or, in an existing clone:
git submodule add https://github.com/Zonit/Zonit.Extensions          external/Zonit.Extensions
git submodule add https://github.com/Zonit/Zonit.Services.Dashboard  external/Zonit.Services.Dashboard
git submodule update --init --recursive

# 2. Build + run.
dotnet build Zonit.Documents.sln -c Release
dotnet run --project Source/Example/Example -c Release --no-build
```

The host listens on `http://localhost:5290` and `https://localhost:7290`.

## What to look at where

| URL                   | What it demonstrates                                            |
|-----------------------|-----------------------------------------------------------------|
| `/`                   | Root site (Zonit.Extensions.Website). Every Example.* area's pages live under `/<area-slug>/...`. |
| `/admin`              | Zonit.Dashboard 10.0.0-preview1 mount with `ComponentsArea`, `MudBlazorArea`, `ValueObjectsArea`. |
| `/admin` → palette icon | Built-in `ThemeSelectorDrawerExtension` — pick Default / Ocean / Forest theme + Auto / Light / Dark mode (persisted to cookies). |
| `/admin/404`          | Dashboard's minimal-layout 404 page (also reached on any unmatched URL). |
| `/components/...`     | Form components, layout slots, page-base composition. |
| `/value-objects/...`  | Strongly-typed primitives showcase (Title, UrlPath, FileSize, Permission, Color). |

## Adding a new demo area

1. Create `Source/Example/Example.YourFeature/` as a Razor class library.
2. Reference the relevant Zonit packages from `external/`:
   ```xml
   <ProjectReference Include="..\..\..\external\Zonit.Extensions\Source\Zonit.Extensions.Website\Zonit.Extensions.Website.csproj" />
   ```
3. Add an `IWebsiteArea` implementation under `YourFeatureArea.cs`.
4. Register in `Source/Example/Example/Program.cs`:
   ```csharp
   builder.Services.AddWebsite(opts => { /* ... */ opts.AddArea<YourFeatureArea>(); });
   app.UseWebsite<App>("/", o => { /* ... */ o.AddArea<YourFeatureArea>(); });
   ```
5. Add to `Zonit.Documents.sln`:
   ```pwsh
   dotnet sln Zonit.Documents.sln add Source/Example/Example.YourFeature/Example.YourFeature.csproj
   ```

## Relationship to Zonit.Sdk

Zonit.Sdk is a meta-repo that aggregates every Zonit subrepo as a submodule
for full-stack development. Zonit.Documents is a sibling consumer — it pulls
only the subrepos it needs (`Zonit.Extensions`, `Zonit.Services.Dashboard`)
and treats them as read-only references.

Editing a Zonit package source while iterating on a demo is supported:
changes inside `external/Zonit.Extensions/Source/...` rebuild on the next
`dotnet build` thanks to `<ProjectReference>` (no NuGet round-trip).
