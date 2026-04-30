using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


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
}
