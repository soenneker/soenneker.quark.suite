using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Collapsible_slots_match_shadcn_base_classes()
    {
        var cut = Render<Collapsible>(parameters => parameters
            .Add(p => p.DefaultOpen, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<CollapsibleTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Toggle")));
                builder.CloseComponent();

                builder.OpenComponent<CollapsibleContent>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Hidden content")));
                builder.CloseComponent();
            })));

        var root = cut.Find("[data-slot='collapsible']");
        var triggerClasses = cut.Find("[data-slot='collapsible-trigger']").GetAttribute("class")!;
        var contentClasses = cut.Find("[data-slot='collapsible-content']").GetAttribute("class")!;

        root.HasAttribute("data-slot").Should().BeTrue();
        triggerClasses.Should().BeNullOrEmpty();
        triggerClasses.Should().NotContain("q-collapsible-trigger");

        contentClasses.Should().BeNullOrEmpty();
        contentClasses.Should().NotContain("q-collapsible-content");
    }

    [Test]
    public void Collapsible_controlled_open_does_not_self_mutate_when_owner_ignores_change()
    {
        var requestedOpen = false;
        var cut = Render<Collapsible>(parameters => parameters
            .Add(p => p.Open, false)
            .Add(p => p.OpenChanged, open => requestedOpen = open)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<CollapsibleTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Toggle")));
                builder.CloseComponent();

                builder.OpenComponent<CollapsibleContent>(2);
                builder.AddAttribute(3, "ForceMount", true);
                builder.AddAttribute(4, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Controlled content")));
                builder.CloseComponent();
            })));

        var trigger = cut.Find("[data-slot='collapsible-trigger']");
        trigger.GetAttribute("aria-expanded").Should().Be("false");

        trigger.Click();

        requestedOpen.Should().BeTrue();
        cut.Find("[data-slot='collapsible-trigger']").GetAttribute("aria-expanded").Should().Be("false");
    }

    [Test]
    public void Collapsible_order_demo_matches_current_shadcn_example_composition()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForCollapsible(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Collapsibles.razor"));
        var orderStart = source.IndexOf("Order summary", StringComparison.Ordinal);
        var basicStart = source.IndexOf("Title=\"Basic\"", StringComparison.Ordinal);
        var orderDemo = source[orderStart..basicStart];

        orderDemo.Should().Contain("Display=\"Display.Flex\" Justify=\"Justify.Center\"");
        orderDemo.Should().Contain("Display=\"Display.Flex\" Width='Width.Token(\"[350px]\")' FlexDirection=\"FlexDirection.Col\" Gap=\"Gap.Is2\"");
        orderDemo.Should().Contain("Display=\"Display.Flex\" ItemsAlign=\"Items.Center\" Justify=\"Justify.Between\" Gap=\"Gap.Is4\" Padding=\"Padding.Is4.OnX\"");
        orderDemo.Should().Contain("@@peduarte starred 3 repositories");
        orderDemo.Should().Contain("Rounded=\"Rounded.Md\" Border=\"Border.Is1\" Padding=\"Padding.Is4.OnX.Is2.OnY\" FontFamily=\"FontFamily.Mono\" TextSize=\"TextSize.Sm\"");
        orderDemo.Should().Contain("<CollapsibleContent Display=\"Display.Flex\" FlexDirection=\"FlexDirection.Col\" Gap=\"Gap.Is2\">");
        orderDemo.Should().Contain("@@radix-ui/primitives");
        orderDemo.Should().Contain("@@radix-ui/colors");
        orderDemo.Should().Contain("@@stitches/react");
        orderDemo.Should().NotContain("Order #4189");
        orderDemo.Should().NotContain("min-h-[220px]");
        orderDemo.Should().NotContain("space-y-2");
    }

    private static string GetSuiteRootForCollapsible()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
