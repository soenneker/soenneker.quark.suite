using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Menubar_slots_match_shadcn_base_classes()
    {
        var root = Render<Menubar>(parameters => parameters.Add(p => p.ChildContent, "Menu"));
        var rootClasses = root.Find("[data-slot='menubar']").GetAttribute("class")!;

        rootClasses.Should().Contain("flex");
        rootClasses.Should().Contain("h-9");
        rootClasses.Should().Contain("items-center");
        rootClasses.Should().Contain("gap-1");
        rootClasses.Should().Contain("rounded-md");
        rootClasses.Should().Contain("border");
        rootClasses.Should().Contain("p-1");
        rootClasses.Should().Contain("bg-background");
        rootClasses.Should().Contain("shadow-xs");

        var source = ReadMenubarSource("MenubarTrigger.razor");
        var rootSource = ReadMenubarSource("Menubar.razor");

        rootSource.Should().Contain("Value=\"@_openMenuValue\"");
        rootSource.Should().Contain("ValueChanged=\"HandleValueChanged\"");

        source.Should().Contain("Rounded.Sm");
        source.Should().Contain("Padding ??= Quark.Padding.Is2.OnX.Is1.OnY");
        source.Should().Contain("focus:bg-accent");
        source.Should().Contain("data-[state=open]:bg-accent");
        source.Should().NotContain("hover:bg-muted");
        source.Should().NotContain("aria-expanded:bg-muted");
    }

    [Test]
    public void Menubar_content_and_items_match_shadcn_v4_classes()
    {
        var content = ReadMenubarSource("MenubarContent.razor");
        var item = ReadMenubarSource("MenubarItem.razor");
        var checkbox = ReadMenubarSource("MenubarCheckboxItem.razor");
        var radio = ReadMenubarSource("MenubarRadioItem.razor");

        content.Should().Contain("ForceMount");
        content.Should().Contain("AlignOffset=\"@AlignOffset\"");
        content.Should().Contain("SideOffset=\"@SideOffset\"");
        content.Should().Contain("CollisionBoundarySelector=\"@CollisionBoundarySelector\"");
        content.Should().Contain("Sticky=\"@Sticky\"");
        content.Should().Contain("MinWidth ??= Quark.MinWidth.Token(\"[12rem]\")");
        content.Should().Contain("Border ??= Quark.Border.Default");
        content.Should().Contain("data-[state=open]:animate-in");
        content.Should().Contain("data-[state=closed]:fade-out-0");
        content.Should().NotContain("data-open:animate-in");
        content.Should().NotContain("max-h-(--radix-menubar-content-available-height)");

        item.Should().Contain("Rounded.Sm");
        item.Should().Contain("data-[disabled]:pointer-events-none");
        item.Should().Contain("data-[inset]:pl-8");
        item.Should().NotContain("group/menubar-item");
        item.Should().Contain("[&_svg:not([class*='text-'])]:text-muted-foreground");

        checkbox.Should().Contain("Rounded.Token(\"xs\")");
        checkbox.Should().Contain("data-slot=\"menubar-checkbox-item-indicator\"");
        checkbox.Should().Contain("absolute left-2 flex size-3.5 items-center justify-center");
        checkbox.Should().Contain("Class=\"size-4\"");
        checkbox.Should().Contain("data-[disabled]:opacity-50");
        checkbox.Should().NotContain("left-1.5");

        radio.Should().Contain("Rounded.Token(\"xs\")");
        radio.Should().Contain("data-slot=\"menubar-radio-item-indicator\"");
        radio.Should().Contain("LucideIcon.Circle");
        radio.Should().Contain("Class=\"size-2 fill-current\"");
        radio.Should().Contain("data-[disabled]:opacity-50");
        radio.Should().NotContain("LucideIcon.Check");
    }

    [Test]
    public void MenubarContent_exposes_radix_popper_collision_options()
    {
        var cut = Render<MenubarContent>(parameters => parameters
            .Add(p => p.CollisionBoundarySelector, "#menubar-boundary-a")
            .Add(p => p.CollisionBoundarySelectors, new[] { "#menubar-boundary-b", "#menubar-boundary-a" })
            .Add(p => p.Sticky, "always")
            .Add(p => p.HideWhenDetached, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Body"))));

        cut.Instance.CollisionBoundarySelector.Should().Be("#menubar-boundary-a");
        cut.Instance.CollisionBoundarySelectors.Should().BeEquivalentTo(["#menubar-boundary-b", "#menubar-boundary-a"]);
        cut.Instance.Sticky.Should().Be("always");
        cut.Instance.HideWhenDetached.Should().BeTrue();
    }

    [Test]
    public void Menubar_demo_examples_match_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForMenubar(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Menubars.razor"));

        source.Should().Contain("Title=\"Basic\"");
        source.Should().Contain("Title=\"With Submenu\"");
        source.Should().Contain("Title=\"With Checkboxes\"");
        source.Should().Contain("Title=\"With Radio\"");
        source.Should().Contain("Title=\"With Icons\"");
        source.Should().Contain("Title=\"With Shortcuts\"");
        source.Should().Contain("Title=\"Format\"");
        source.Should().Contain("Title=\"Insert\"");
        source.Should().Contain("Title=\"Destructive\"");
        source.Should().Contain("Title=\"In Dialog\"");
        source.Should().Contain("Title=\"With Inset\"");
        source.Should().Contain("<MenubarGroup>");
        source.Should().Contain("<MenubarContent Class=\"w-64\">");
        source.Should().Contain("Always Show Bookmarks Bar");
        source.Should().Contain("Superscript");
        source.Should().Contain("Value=\"@_selectedTheme\"");
        source.Should().Contain("Find &amp; Replace");
        source.Should().Contain("Destructive=\"true\"");
        source.Should().Contain("Menubar Example");
        source.Should().Contain("More Options");
    }

    private static string ReadMenubarSource(string fileName)
    {
        return File.ReadAllText(Path.Combine(GetSuiteRootForMenubar(), "src", "Soenneker.Quark.Suite", "Components", "Menubar", fileName));
    }

    private static string GetSuiteRootForMenubar()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }

}
