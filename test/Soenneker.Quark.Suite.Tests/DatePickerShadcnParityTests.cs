using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DatePicker_matches_shadcn_trigger_and_panel_contract()
    {
        var cut = Render<DatePicker>();

        var trigger = cut.Find("button[data-slot='popover-trigger']");
        var hiddenInput = cut.Find("input[type='hidden']");

        string triggerClasses = trigger.GetAttribute("class")!;

        triggerClasses.Should().Contain("group/button");
        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("shrink-0");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("bg-clip-padding");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("whitespace-nowrap");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("select-none");
        triggerClasses.Should().Contain("focus-visible:border-ring");
        triggerClasses.Should().Contain("focus-visible:ring-3");
        triggerClasses.Should().Contain("focus-visible:ring-ring/50");
        triggerClasses.Should().Contain("active:not-aria-[haspopup]:translate-y-px");
        triggerClasses.Should().Contain("disabled:pointer-events-none");
        triggerClasses.Should().Contain("disabled:opacity-50");
        triggerClasses.Should().Contain("aria-invalid:border-destructive");
        triggerClasses.Should().Contain("aria-invalid:ring-3");
        triggerClasses.Should().Contain("aria-invalid:ring-destructive/20");
        triggerClasses.Should().Contain("[&_svg]:pointer-events-none");
        triggerClasses.Should().Contain("[&_svg]:shrink-0");
        triggerClasses.Should().Contain("[&_svg:not([class*='size-'])]:size-4");
        triggerClasses.Should().Contain("border-border");
        triggerClasses.Should().Contain("bg-background");
        triggerClasses.Should().Contain("hover:bg-muted");
        triggerClasses.Should().Contain("hover:text-foreground");
        triggerClasses.Should().Contain("aria-expanded:bg-muted");
        triggerClasses.Should().Contain("aria-expanded:text-foreground");
        triggerClasses.Should().Contain("dark:border-input");
        triggerClasses.Should().Contain("dark:bg-input/30");
        triggerClasses.Should().Contain("dark:hover:bg-input/50");
        triggerClasses.Should().Contain("h-8");
        triggerClasses.Should().Contain("gap-1.5");
        triggerClasses.Should().Contain("px-2.5");
        triggerClasses.Should().Contain("has-data-[icon=inline-end]:pr-2");
        triggerClasses.Should().Contain("has-data-[icon=inline-start]:pl-2");
        triggerClasses.Should().Contain("w-[212px]");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("text-left");
        triggerClasses.Should().Contain("font-normal");
        triggerClasses.Should().Contain("data-[empty=true]:text-muted-foreground");
        triggerClasses.Should().NotContain("w-[280px]");
        triggerClasses.Should().NotContain("justify-start");

        trigger.TextContent.Should().Contain("Pick a date");
        hiddenInput.GetAttribute("value").Should().BeEmpty();

        trigger.Click();

        var panel = cut.Find("[data-slot='popover-content']");
        string panelClasses = panel.GetAttribute("class")!;

        panelClasses.Should().Contain("bg-popover");
        panelClasses.Should().Contain("text-popover-foreground");
        panelClasses.Should().Contain("rounded-md");
        panelClasses.Should().Contain("border");
        panelClasses.Should().Contain("w-auto");
        panelClasses.Should().Contain("p-0");
    }
}
