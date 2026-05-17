using Extensions.ValueObjects.Pages;
using Zonit.Extensions.Website;

namespace Documents;

public sealed class ValueObjectsArea : IWebsiteArea
{
    public string Key => "value-objects";

    public IReadOnlyList<NavGroup> Navigation { get; } =
    [
        new NavGroup
        {
            Title = "Value objects",
            Order = 70,
            Children =
            [
                new NavItem { Title = "Strings",  Url = Strings.Route    },
                new NavItem { Title = "Numbers",  Url = Numbers.Route    },
                new NavItem { Title = "Files",    Url = Files.Route      },
                new NavItem { Title = "Time",     Url = Time.Route       },
                new NavItem { Title = "Identity", Url = IdentityVo.Route },
                new NavItem { Title = "Tenancy",  Url = TenancyVo.Route  },
            ],
        },
    ];
}
