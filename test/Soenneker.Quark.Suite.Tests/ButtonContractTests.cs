using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Button_with_href_renders_anchor_href()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.Href, "/dashboard")
            .Add(p => p.ChildContent, "Dashboard"));

        var anchor = cut.Find("a");

        anchor.GetAttribute("href").Should().Be("/dashboard");
    }

    [Test]
    public void Button_interface_exposes_href()
    {
        typeof(IButton).GetProperty(nameof(IButton.Href)).Should().NotBeNull();
    }

    [Test]
    public void Button_no_longer_exposes_to()
    {
        typeof(Button).GetProperty("To").Should().BeNull();
        typeof(IButton).GetProperty("To").Should().BeNull();
    }

    [Test]
    public void Link_components_expose_href_not_to()
    {
        System.Type[] linkComponentTypes =
        [
            typeof(BreadcrumbLink),
            typeof(BreadcrumbItem),
            typeof(Header.HeaderBrand),
            typeof(Header.HeaderActionLink),
            typeof(PaginationLink),
            typeof(PaginationPrevious),
            typeof(PaginationNext),
            typeof(SidebarMenuButton),
            typeof(SidebarMenuSubButton)
        ];

        foreach (var type in linkComponentTypes)
        {
            type.GetProperty("Href").Should().NotBeNull($"{type.Name} should use Href for URL targets");
            type.GetProperty("To").Should().BeNull($"{type.Name} should not expose To for URL targets");
        }

        typeof(IBreadcrumbItem).GetProperty(nameof(IBreadcrumbItem.Href)).Should().NotBeNull();
        typeof(IBreadcrumbItem).GetProperty("To").Should().BeNull();
    }
}
