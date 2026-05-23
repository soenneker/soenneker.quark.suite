using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Stepper_emits_reui_slots_orientation_and_state_classes()
    {
        var cut = Render<Stepper>(parameters => parameters
            .Add(p => p.Value, 2)
            .Add(p => p.ChildContent, BuildStepper()));

        var stepper = cut.Find("[data-slot='stepper']");
        stepper.TagName.Should().Be("DIV");
        stepper.GetAttribute("role").Should().Be("tablist");
        stepper.GetAttribute("aria-orientation").Should().Be("horizontal");
        stepper.GetAttribute("data-orientation").Should().Be("horizontal");

        var nav = cut.Find("[data-slot='stepper-nav']");
        nav.GetAttribute("data-state").Should().Be("2");
        nav.GetAttribute("class").Should().Contain("group/stepper-nav");

        var items = cut.FindAll("[data-slot='stepper-item']");
        items.Should().HaveCount(3);
        items[0].GetAttribute("data-state").Should().Be("completed");
        items[1].GetAttribute("data-state").Should().Be("active");
        items[2].GetAttribute("data-state").Should().Be("inactive");

        var trigger = items[1].QuerySelector("[data-slot='stepper-trigger']");
        trigger.Should().NotBeNull();
        trigger!.GetAttribute("role").Should().Be("tab");
        trigger.GetAttribute("aria-selected").Should().Be("true");
        trigger.GetAttribute("aria-controls").Should().Be("stepper-panel-2");
        trigger.GetAttribute("data-state").Should().Be("active");

        cut.Find("[data-slot='stepper-indicator']").GetAttribute("class").Should().Contain("data-[state=active]:bg-primary");
        cut.Find("[data-slot='stepper-separator']").GetAttribute("class").Should().Contain("group-data-[orientation=horizontal]/stepper-nav:flex-1");
        cut.Find("[data-slot='stepper-title']").GetAttribute("class").Should().Contain("font-medium");
        cut.Find("[data-slot='stepper-description']").GetAttribute("class").Should().Contain("text-muted-foreground");

        var content = cut.Find("[data-slot='stepper-content']");
        content.GetAttribute("role").Should().Be("tabpanel");
        content.GetAttribute("id").Should().Be("stepper-panel-2");
        content.TextContent.Should().Contain("Security");
    }

    private static RenderFragment BuildStepper()
    {
        return builder =>
        {
            builder.OpenComponent<StepperNav>(0);
            builder.AddAttribute(1, nameof(StepperNav.ChildContent), BuildNav());
            builder.CloseComponent();

            builder.OpenComponent<StepperPanel>(2);
            builder.AddAttribute(3, nameof(StepperPanel.ChildContent), (RenderFragment) (panelBuilder =>
            {
                panelBuilder.OpenComponent<StepperContent>(0);
                panelBuilder.AddAttribute(1, nameof(StepperContent.Value), 1);
                panelBuilder.AddAttribute(2, nameof(StepperContent.ChildContent), (RenderFragment) (contentBuilder => contentBuilder.AddContent(0, "Account")));
                panelBuilder.CloseComponent();

                panelBuilder.OpenComponent<StepperContent>(3);
                panelBuilder.AddAttribute(4, nameof(StepperContent.Value), 2);
                panelBuilder.AddAttribute(5, nameof(StepperContent.ChildContent), (RenderFragment) (contentBuilder => contentBuilder.AddContent(0, "Security")));
                panelBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        };
    }

    private static RenderFragment BuildNav()
    {
        return builder =>
        {
            AddItem(builder, 0, 1, "Account", "Create profile");
            AddItem(builder, 10, 2, "Security", "Verify access");
            AddItem(builder, 20, 3, "Finish", "Review setup");
        };
    }

    private static void AddItem(RenderTreeBuilder builder, int sequence, int step, string title, string description)
    {
        builder.OpenComponent<StepperItem>(sequence);
        builder.AddAttribute(sequence + 1, nameof(StepperItem.Step), step);
        builder.AddAttribute(sequence + 2, nameof(StepperItem.ChildContent), (RenderFragment) (itemBuilder =>
        {
            itemBuilder.OpenComponent<StepperTrigger>(0);
            itemBuilder.AddAttribute(1, nameof(StepperTrigger.ChildContent), (RenderFragment) (triggerBuilder =>
            {
                triggerBuilder.OpenComponent<StepperIndicator>(0);
                triggerBuilder.AddAttribute(1, nameof(StepperIndicator.ChildContent), (RenderFragment) (indicatorBuilder => indicatorBuilder.AddContent(0, step)));
                triggerBuilder.CloseComponent();

                triggerBuilder.OpenComponent<StepperTitle>(2);
                triggerBuilder.AddAttribute(3, nameof(StepperTitle.ChildContent), (RenderFragment) (titleBuilder => titleBuilder.AddContent(0, title)));
                triggerBuilder.CloseComponent();

                triggerBuilder.OpenComponent<StepperDescription>(4);
                triggerBuilder.AddAttribute(5, nameof(StepperDescription.ChildContent), (RenderFragment) (descriptionBuilder => descriptionBuilder.AddContent(0, description)));
                triggerBuilder.CloseComponent();
            }));
            itemBuilder.CloseComponent();

            itemBuilder.OpenComponent<StepperSeparator>(2);
            itemBuilder.CloseComponent();
        }));
        builder.CloseComponent();
    }
}
