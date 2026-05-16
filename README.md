# Zonit.Documents

Live, runnable documentation for the entire **Zonit** stack. Every public package
gets its own demo project under `Source/Extensions.*` and they all mount into a
single host (`Source/Documents`), so opening any page in the running site doubles
as the page's spec, screenshot, and integration test.

The same host also mounts **Zonit.Dashboard** at `/admin`, so the dashboard's
themes, drawer extensions, navigation aggregation, and per-mount options are
testable end-to-end in one process.

This repository is **a submodule of `Zonit.Sdk`** — it must be checked out inside
the Sdk meta-repo at `Sdk/Source/Documents/Zonit.Documents/` for the
`<ProjectReference>` paths to resolve.

## Repository layout

```
Zonit.Documents/                            (this repo, lives as a submodule)
├── Directory.Build.props                   IsZonitSdkWorkspace=true fallback.
├── Zonit.Documents.sln                     10 projects: host + 9 demo areas.
├── Docs/                                   Long-form Markdown (concepts, ADRs, design notes).
└── Source/
    ├── Documents/                          ASP.NET Core host (Program.cs, App.razor, Routes.razor).
    ├── Extensions.Auth/                    IAuthSource demo — login, logout, gated pages.
    ├── Extensions.Components/              Zonit.Extensions.Website ZonitText* + layout slots.
    ├── Extensions.Cultures/                ICultureProvider + language switcher.
    ├── Extensions.MudBlazor/               MudBlazor adapter components.
    ├── Extensions.Organizations/           IOrganizationSource demo.
    ├── Extensions.Projects/                IProjectSource demo.
    ├── Extensions.Shared/                  Helpers reused by multiple demo areas.
    ├── Extensions.Tenants/                 ITenantSource demo (multi-tenant routing).
    └── Extensions.ValueObjects/            Title / UrlPath / FileSize / Permission / Color demos.
```

## How the cross-repo references resolve

Every demo project under `Source/Extensions.*` has a `<ProjectReference>` pointing
four directories up and back down into Sdk's sibling submodules:

```xml
<!-- Source/Extensions.Auth/Extensions.Auth.csproj -->
<ProjectReference Include="..\..\..\..\Extensions\Zonit.Extensions\Source\Zonit.Extensions.Website\Zonit.Extensions.Website.csproj" />
```

The four `..\` segments climb out of `Sdk/Source/Documents/Zonit.Documents/Source/Extensions.Auth/`
and land on `Sdk/Source/`, from where `Extensions/Zonit.Extensions/...` and
`Services/Zonit.Services.Dashboard/...` are immediate siblings.

That path resolves only when this repo lives at `Sdk/Source/Documents/Zonit.Documents/`,
which is why standalone checkouts do not build. Opening the solution while the Sdk
meta-repo is checked out around it brings everything online.

## First-time setup

```pwsh
# Clone the Sdk meta-repo recursively. Once Zonit.Documents is registered as a
# submodule in Sdk, this single command pulls every dependency.
git clone --recurse-submodules https://github.com/Zonit/Zonit.Sdk

# If Sdk is already cloned, add Documents as a submodule under the conventional path:
cd Zonit.Sdk
git submodule add https://github.com/Zonit/Documents Source/Documents/Zonit.Documents
git submodule update --init --recursive

# Build + run.
cd Source/Documents/Zonit.Documents
dotnet build Zonit.Documents.sln -c Release
dotnet run --project Source/Documents -c Release --no-build
```

The host listens on `http://localhost:5290` and `https://localhost:7290`.

## What to look at where

| URL                       | What it demonstrates                                            |
|---------------------------|-----------------------------------------------------------------|
| `/`                       | Root site (Zonit.Extensions.Website). Each Extensions.* area's pages live under `/<area-slug>/...`. |
| `/admin`                  | Zonit.Dashboard 10.0.0-preview1 mount with `ComponentsArea`, `MudBlazorArea`, `ValueObjectsArea`. |
| `/admin` → palette icon   | Built-in `ThemeSelectorDrawerExtension` — pick Default / Ocean / Forest + Auto / Light / Dark (persisted to cookies). |
| `/admin/404`              | Dashboard's minimal-layout 404 page (also reached on any unmatched URL). |
| `/components/...`         | Form components, layout slots, page-base composition. |
| `/value-objects/...`      | Strongly-typed primitives showcase (Title, UrlPath, FileSize, Permission, Color). |

## Adding a new demo

1. Create `Source/Extensions.YourFeature/` as a Razor class library.
2. Reference the matching Zonit package from sibling submodules:
   ```xml
   <ProjectReference Include="..\..\..\..\..\Source\Extensions\Zonit.Extensions\Source\Zonit.Extensions.YourFeature\Zonit.Extensions.YourFeature.csproj" />
   ```
3. Add an `IWebsiteArea` implementation as `YourFeatureArea.cs`. Convention is
   `namespace Documents` for the Area class itself (so the host can `using Documents`
   and see every Area without per-project imports).
4. Register in `Source/Documents/Program.cs`:
   ```csharp
   builder.Services.AddWebsite(opts => { /* ... */ opts.AddArea<YourFeatureArea>(); });
   app.UseWebsite<App>("/", o => { /* ... */ o.AddArea<YourFeatureArea>(); });
   ```
5. Add to `Zonit.Documents.sln`:
   ```pwsh
   dotnet sln Zonit.Documents.sln add Source/Extensions.YourFeature/Extensions.YourFeature.csproj
   ```

## Conventions

- **Project naming** — `Extensions.<Domain>` for demo areas, `Documents` for the host.
  The repo name (`Zonit.Documents`) only appears at the solution/folder level.
- **Namespaces** — All `*Area` classes live in `namespace Documents` regardless of
  which `Extensions.*` project they ship in. Project-internal helpers (models,
  components) live in `namespace Extensions.<Domain>.<Sub>`.
- **No NuGet** — every Zonit package is referenced through `<ProjectReference>` into
  sibling Sdk submodules, so editing Zonit.* source rebuilds on the next
  `dotnet build` with no round-trip.

## Leftovers from initial scaffolding

The `Source/Example/` folder is empty — it is the old container that wrapped the
projects before they were flattened to `Source/Extensions.*`. Same for `external/`,
which was an earlier (rejected) plan to host submodules locally inside this repo.
Neither folder is referenced by anything; leaving them in place per the operator's
"don't delete anything" instruction. Safe to `Remove-Item` whenever convenient.
