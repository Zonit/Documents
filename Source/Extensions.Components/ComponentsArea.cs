using Extensions.Components.Pages;
using Microsoft.Extensions.DependencyInjection;
using Zonit.Extensions;
using Zonit.Extensions.Website;

namespace Documents;

public sealed class ComponentsArea : IWebsiteArea, IWebsiteServices
{
    public string Key => "components";

    public void ConfigureServices(IServiceCollection services)
    {
        // Demo registration — the plug-in pages reference "Demo.Box" as a string only.
        // The concrete layout type (DemoBoxLayout) lives in this area's assembly; any
        // page in any other assembly can [LayoutKey("Demo.Box")] without referencing it.
        services.AddWebsiteLayout<Example.Layouts.DemoBoxLayout>("Demo.Box");
    }

    public IReadOnlyList<NavGroup> Navigation { get; } =
    [
        new NavGroup
        {
            Title = "Components",
            Order = 60,
            Children =
            [
                new NavItem { Title = "PageViewBase<T>",  Url = PageView.Route        },
                new NavItem { Title = "PageEditBase<T>",  Url = PageEdit.Route        },
                new NavItem { Title = "AutoSave",         Url = AutoSave.Route        },
                new NavItem { Title = "Toasts",           Url = Toasts.Route          },
                new NavItem { Title = "Breadcrumbs",      Url = BreadcrumbsDemo.Route },
                new NavItem { Title = "Cookies",          Url = Cookies.Route         },
                new NavItem { Title = "Layouts",          Url = Layouts.Route         },
                new NavItem { Title = "State extensions", Url = StateExtensions.Route },
            ],
        },
    ];
}
