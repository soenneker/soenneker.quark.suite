using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Slider_styles_target_bradix_orientation_attribute_directly()
    {
        var cut = Render<Slider>(parameters => parameters.Add(component => component.Orientation, Orientation.Vertical));

        var root = cut.Find("[data-slot='slider']");
        var track = cut.Find("[data-slot='slider-track']");

        root.GetAttribute("data-orientation").Should().Be("vertical");
        root.GetAttribute("class").Should().Contain("data-[orientation=vertical]:flex-col");
        root.GetAttribute("class").Should().NotContain("data-vertical:");
        track.GetAttribute("data-orientation").Should().Be("vertical");
        track.GetAttribute("class").Should().Contain("data-[orientation=vertical]:w-1");
        track.GetAttribute("class").Should().NotContain("data-vertical:");
    }

    [Test]
    public void Separator_styles_target_bradix_orientation_attribute_directly()
    {
        var cut = Render<Separator>(parameters => parameters.Add(component => component.Orientation, SeparatorOrientation.Vertical));

        var separator = cut.Find("[data-slot='separator']");

        separator.GetAttribute("data-orientation").Should().Be("vertical");
        separator.GetAttribute("class").Should().Contain("data-[orientation=vertical]:w-px");
        separator.GetAttribute("class").Should().NotContain("data-vertical:");
    }

    [Test]
    public void Field_label_checked_styles_target_standard_state_attribute()
    {
        var cut = Render<FieldLabel>();
        var label = cut.Find("[data-slot='field-label']");

        label.GetAttribute("class").Should().Contain("has-[[data-state=checked]]:border-primary/30");
        label.GetAttribute("class").Should().NotContain("has-data-checked:");
    }

    [Test]
    public void Toggle_group_styles_target_bradix_orientation_attribute_directly()
    {
        var cut = Render<ToggleGroup>(parameters => parameters
            .Add(component => component.Orientation, Orientation.Vertical)
            .Add(component => component.ChildContent, builder =>
            {
                builder.OpenComponent<ToggleGroupItem>(0);
                builder.AddAttribute(1, nameof(ToggleGroupItem.Value), "bold");
                builder.CloseComponent();
            }));

        var group = cut.Find("[data-slot='toggle-group']");
        var item = cut.Find("[data-slot='toggle-group-item']");

        group.GetAttribute("data-orientation").Should().Be("vertical");
        group.GetAttribute("class").Should().Contain("data-[orientation=vertical]:flex-col");
        item.GetAttribute("class").Should().Contain("group-data-[orientation=vertical]/toggle-group:data-[spacing=0]:first:rounded-t-lg");
    }

    [Test]
    public void Navigation_menu_trigger_styles_target_bradix_open_state_directly()
    {
        var cut = Render<NavigationMenu>(parameters => parameters.Add(component => component.ChildContent, BuildNavigationMenu()));
        var trigger = cut.Find("[data-slot='navigation-menu-trigger']");

        trigger.GetAttribute("data-state").Should().Be("closed");
        trigger.GetAttribute("class").Should().Contain("data-[state=open]:bg-muted/50");
        trigger.GetAttribute("class").Should().NotContain("data-open:");
        trigger.GetAttribute("class").Should().NotContain("data-popup-open:");
    }

    private static RenderFragment BuildNavigationMenu()
    {
        return builder =>
        {
            builder.OpenComponent<NavigationMenuList>(0);
            builder.AddAttribute(1, nameof(NavigationMenuList.ChildContent), (RenderFragment) (listBuilder =>
            {
                listBuilder.OpenComponent<NavigationMenuItem>(0);
                listBuilder.AddAttribute(1, nameof(NavigationMenuItem.ChildContent), (RenderFragment) (itemBuilder =>
                {
                    itemBuilder.OpenComponent<NavigationMenuTrigger>(0);
                    itemBuilder.AddAttribute(1, nameof(NavigationMenuTrigger.ChildContent), (RenderFragment) (content => content.AddContent(0, "Menu")));
                    itemBuilder.CloseComponent();
                }));
                listBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        };
    }
}
