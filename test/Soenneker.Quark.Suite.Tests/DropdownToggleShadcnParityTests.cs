using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DropdownToggle_default_mode_matches_current_outline_trigger_shell()
    {
        var cut = Render<DropdownToggle>(parameters => parameters
            .Add(p => p.ChildContent, "Open"));

        var classes = cut.Find("[data-slot='dropdown-menu-trigger']").GetAttribute("class")!;

        classes.Should().BeNullOrEmpty();
        cut.Find("[data-slot='dropdown-menu-trigger']").GetAttribute("aria-haspopup").Should().Be("menu");
        cut.Find("[data-slot='dropdown-menu-trigger']").GetAttribute("data-state").Should().Be("closed");
    }

    [Test]
    public void DropdownToggle_split_mode_matches_current_button_shell()
    {
        var cut = Render<DropdownToggle>(parameters => parameters
            .Add(p => p.IsSplit, true)
            .Add(p => p.ChildContent, "More"));

        var classes = cut.Find("[data-slot='dropdown-menu-trigger']").GetAttribute("class")!;

        classes.Should().BeNullOrEmpty();
        cut.Find("[data-slot='dropdown-menu-trigger']").GetAttribute("aria-haspopup").Should().Be("menu");
        cut.Find("[data-slot='dropdown-menu-trigger']").GetAttribute("data-state").Should().Be("closed");
    }

    [Test]
    public void DropdownToggle_as_child_button_does_not_leak_trigger_context_to_descendants()
    {
        var cut = Render<Dropdown>(parameters => parameters
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<DropdownToggle>(0);
                builder.AddAttribute(1, nameof(DropdownToggle.AsChild), true);
                builder.AddAttribute(2, nameof(DropdownToggle.ChildContent), (RenderFragment)(toggleBuilder =>
                {
                    toggleBuilder.OpenComponent<Button>(3);
                    toggleBuilder.AddAttribute(4, nameof(Button.ChildContent), (RenderFragment)(buttonBuilder =>
                    {
                        buttonBuilder.OpenComponent<BreadcrumbEllipsis>(5);
                        buttonBuilder.CloseComponent();
                        buttonBuilder.OpenComponent<Span>(6);
                        buttonBuilder.AddAttribute(7, "class", "sr-only");
                        buttonBuilder.AddAttribute(8, nameof(Span.ChildContent), (RenderFragment)(spanBuilder => spanBuilder.AddContent(9, "Toggle menu")));
                        buttonBuilder.CloseComponent();
                    }));
                    toggleBuilder.CloseComponent();
                }));
                builder.CloseComponent();

                builder.OpenComponent<DropdownMenu>(12);
                builder.AddAttribute(13, nameof(DropdownMenu.ChildContent), (RenderFragment)(menuBuilder =>
                {
                    menuBuilder.OpenComponent<DropdownItem>(14);
                    menuBuilder.AddAttribute(15, nameof(DropdownItem.ChildContent), (RenderFragment)(itemBuilder => itemBuilder.AddContent(16, "Documentation")));
                    menuBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        var trigger = cut.Find("[data-slot='dropdown-menu-trigger']");
        var triggerId = trigger.GetAttribute("id")!;
        var controls = trigger.GetAttribute("aria-controls")!;

        System.Linq.Enumerable.Count(cut.FindAll("*"), element => element.Id == triggerId).Should().Be(1);
        System.Linq.Enumerable.Count(cut.FindAll("*"), element => element.GetAttribute("aria-controls") == controls).Should().Be(1);

        trigger.Click();

        cut.Find("[data-slot='dropdown-menu-content']").TextContent.Should().Contain("Documentation");
    }
}
