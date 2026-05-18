using AwesomeAssertions;
using Bunit;
using System.Linq;

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
    public void ButtonSize_supports_tokens_and_builder_values()
    {
        ButtonSize.Default.Should().Be("default");
        ButtonSize.Xs.Should().Be("xs");
        ButtonSize.Sm.Should().Be("sm");
        ButtonSize.Lg.Should().Be("lg");
        ButtonSize.Icon.Should().Be("icon");
        ButtonSize.IconXs.Should().Be("icon-xs");
        ButtonSize.IconSm.Should().Be("icon-sm");
        ButtonSize.IconLg.Should().Be("icon-lg");

        typeof(ButtonSize).GetMethods()
            .Where(method => method.Name == "op_Implicit" && method.ReturnType.Name.StartsWith("CssValue", System.StringComparison.Ordinal))
            .Should()
            .ContainSingle();
    }

    [Test]
    public void Button_accepts_responsive_button_size_builder()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.ButtonSize, ButtonSize.Xs.OnLg.Sm.On2xl.Lg)
            .Add(p => p.ChildContent, "Responsive"));

        var button = cut.Find("button");
        var classes = button.GetAttribute("class");

        classes.Should().Contain("h-6");
        classes.Should().Contain("lg:h-7");
        classes.Should().Contain("2xl:h-9");
        button.HasAttribute("data-size").Should().BeFalse();
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
