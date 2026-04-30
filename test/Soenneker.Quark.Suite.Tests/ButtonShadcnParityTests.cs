using AwesomeAssertions;
using Bunit;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Button_default_matches_shadcn_base_classes_exactly()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.ChildContent, "Button"));

        var classes = cut.Find("[data-slot='button']").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("whitespace-nowrap");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("outline-none");
        classes.Should().Contain("focus-visible:border-ring");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().Contain("h-9");
        classes.Should().Contain("px-4");
        classes.Should().Contain("py-2");
        classes.Should().Contain("has-[>svg]:px-3");
        classes.Should().Contain("bg-primary");
        classes.Should().Contain("text-primary-foreground");
        classes.Should().Contain("hover:bg-primary/90");
        classes.Should().NotContain("group/button");
        classes.Should().NotContain("rounded-lg");
        classes.Should().NotContain("border-transparent");
        classes.Should().NotContain("bg-clip-padding");
        classes.Should().NotContain("select-none");
        classes.Should().NotContain("active:not-aria-[haspopup]:translate-y-px");
        classes.Should().NotContain("h-8");
        classes.Should().NotContain("px-2.5");
        classes.Should().NotContain("[a]:hover:bg-primary/80");
        classes.Should().NotContain("rounded-lg!");
        classes.Should().NotContain("border-1");
    }


    [Test]
    public void Button_outline_icon_matches_shadcn_base_classes_exactly()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Outline)
            .Add(p => p.Size, ButtonSize.Icon)
            .AddUnmatched("aria-label", "Submit")
            .Add(p => p.ChildContent, "Icon"));

        var classes = cut.Find("[data-slot='button']").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("shadow-xs");
        classes.Should().Contain("hover:bg-accent");
        classes.Should().Contain("hover:text-accent-foreground");
        classes.Should().Contain("dark:border-input");
        classes.Should().Contain("dark:bg-input/30");
        classes.Should().Contain("dark:hover:bg-input/50");
        classes.Should().Contain("size-9");
        classes.Should().NotContain("border-1");
        classes.Should().NotContain("group/button");
        classes.Should().NotContain("rounded-lg");
        classes.Should().NotContain("select-none");
        classes.Should().NotContain("focus-visible:ring-3");
        classes.Should().NotContain("border-border");
        classes.Should().NotContain("hover:bg-muted");
        classes.Should().NotContain("hover:text-foreground");
        classes.Should().NotContain("aria-expanded:bg-muted");
        classes.Should().NotContain("aria-expanded:text-foreground");
        classes.Should().NotContain("size-8");
    }

    [Test]
    public void Button_demo_examples_match_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForButton(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Buttons.razor"));

        source.Should().Contain("Displays a button or a component that looks like a button.");
        source.Should().Contain("Remember to add the data-icon attribute to the icon for the correct spacing.");
        source.Should().Contain("<Icon Name=\"LucideIcon.GitBranch\" data-icon=\"inline-start\" />");
        source.Should().NotContain("LucideIcon.GitFork");
        source.Should().Contain("Use the rounded-full class to make the button rounded.");
        source.Should().Contain("<Button Size=\"ButtonSize.Icon\" Rounded=\"Rounded.Full\" aria-label=\"Submit\">");
        source.Should().Contain("<Spinner data-icon=\"inline-start\" />");
        source.Should().Contain("<Spinner data-icon=\"inline-end\" />");
        source.Should().Contain("<ButtonGroup AriaLabel=\"Message actions\">");
        source.Should().Contain("<Button Variant=\"ButtonVariant.Outline\">Archive</Button>");
        source.Should().Contain("<DropdownToggle AsChild=\"true\">");
        source.Should().Contain("<Button Variant=\"ButtonVariant.Outline\" Size=\"ButtonSize.Icon\" aria-label=\"More Options\">");
        source.Should().NotContain("<DropdownToggle IsSplit=\"true\" aria-label=\"More Options\">");
        source.Should().Contain("<Anchor To=\"/login\">Login</Anchor>");
        source.Should().Contain("<Button Variant=\"ButtonVariant.Destructive\">حذف</Button>");
        source.Should().Contain("<Icon Name=\"LucideIcon.ArrowRight\" data-icon=\"inline-end\" />");
        source.Should().Contain("<Button Disabled=\"true\">");
    }

    private static string GetSuiteRootForButton()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }

}

