using AwesomeAssertions;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DatePicker_matches_shadcn_trigger_and_panel_contract()
    {
        var root = GetSuiteRootForDatePicker();
        var source = File.ReadAllText(Path.Combine(root, "src", "Soenneker.Quark.Suite", "Components", "Forms", "DatePicker.razor"));
        var trigger = File.ReadAllText(Path.Combine(root, "src", "Soenneker.Quark.Suite", "Components", "Popover", "PopoverTrigger.razor"));
        var popoverContent = File.ReadAllText(Path.Combine(root, "src", "Soenneker.Quark.Suite", "Components", "Popover", "PopoverContent.razor"));

        trigger.Should().Contain("Display ??= Quark.Display.InlineFlex");
        trigger.Should().Contain("Shrink ??= Quark.Shrink.Is0");
        trigger.Should().Contain("ItemsAlign ??= Items.Center");
        trigger.Should().Contain("Rounded ??= Quark.Rounded.Lg");
        trigger.Should().Contain("Border ??= Quark.Border.Default");
        trigger.Should().Contain("TextSize ??= Quark.TextSize.Sm");
        trigger.Should().Contain("Whitespace ??= Quark.Whitespace.Nowrap");
        trigger.Should().Contain("Transition ??= Quark.Transition.All");
        trigger.Should().Contain("OutlineStyle ??= Quark.OutlineStyle.None");
        trigger.Should().Contain("focus-visible:border-ring focus-visible:ring-3 focus-visible:ring-ring/50");
        trigger.Should().Contain("dark:border-input dark:bg-input/30 dark:hover:bg-input/50");
        source.Should().Contain("w-[212px]");
        source.Should().Contain("justify-between");
        source.Should().Contain("text-left");
        source.Should().Contain("font-normal");
        source.Should().Contain("data-[empty=true]:text-muted-foreground");
        source.Should().Contain("Pick a date");
        source.Should().Contain("type=\"hidden\"");
        popoverContent.Should().Contain("BackgroundColor ??= Quark.BackgroundColor.Popover");
        popoverContent.Should().Contain("TextColor ??= Quark.TextColor.PopoverForeground");
        popoverContent.Should().Contain("Rounded ??= Quark.Rounded.Md");
        popoverContent.Should().Contain("Border ??= Quark.Border.Default");
        source.Should().Contain("Width=\"Quark.Width.Auto\"");
        source.Should().Contain("Padding=\"Quark.Padding.Is0\"");
    }

    private static string GetSuiteRootForDatePicker()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
