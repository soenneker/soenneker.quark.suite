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
        var popoverContent = File.ReadAllText(Path.Combine(root, "src", "Soenneker.Quark.Suite", "Components", "Popover", "PopoverContent.razor"));

        source.Should().Contain("<PopoverTrigger AsChild=\"true\">");
        source.Should().Contain("Variant=\"ButtonVariant.Outline\"");
        source.Should().Contain("w-[280px]");
        source.Should().Contain("justify-start");
        source.Should().Contain("text-left");
        source.Should().Contain("font-normal");
        source.Should().Contain("data-[empty=true]:text-muted-foreground");
        source.Should().Contain("LucideIcon.Calendar");
        source.Should().NotContain("LucideIcon.ChevronDown");
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
