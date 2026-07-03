using System;
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

        ShouldContainClasses(cut.Find("[data-slot='stepper-indicator']").GetAttribute("class"), "bg-primary", "text-primary-foreground");
        var separatorClass = cut.Find("[data-slot='stepper-separator']").GetAttribute("class");
        ShouldContainClasses(separatorClass, "h-0.5", "flex-1");
        cut.Find("[data-slot='stepper-title']").GetAttribute("class").Should().Contain("font-medium");
        cut.Find("[data-slot='stepper-description']").GetAttribute("class").Should().Contain("text-muted-foreground");

        var content = cut.Find("[data-slot='stepper-content']");
        content.GetAttribute("role").Should().Be("tabpanel");
        content.GetAttribute("id").Should().Be("stepper-panel-2");
        content.TextContent.Should().Contain("Security");
    }

    [Test]
    public void Stepper_indicator_uses_state_templates_with_loading_priority()
    {
        var cut = Render<Stepper>(parameters => parameters
            .Add(p => p.Value, 2)
            .Add(p => p.CompletedIndicator, (RenderFragment) (builder => builder.AddContent(0, "done")))
            .Add(p => p.LoadingIndicator, (RenderFragment) (builder => builder.AddContent(0, "loading")))
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<StepperNav>(0);
                builder.AddAttribute(1, nameof(StepperNav.ChildContent), (RenderFragment) (navBuilder =>
                {
                    AddTemplatedIndicatorItem(navBuilder, 0, 1, false);
                    AddTemplatedIndicatorItem(navBuilder, 10, 2, true);
                }));
                builder.CloseComponent();
            }));

        var indicators = cut.FindAll("[data-slot='stepper-indicator']");
        indicators.Should().HaveCount(2);
        indicators[0].TextContent.Should().Contain("done");
        indicators[1].TextContent.Should().Contain("loading");
    }

    [Test]
    public void Stepper_trigger_as_child_renders_non_interactive_wrapper()
    {
        var cut = Render<Stepper>(parameters => parameters
            .Add(p => p.Value, 1)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<StepperNav>(0);
                builder.AddAttribute(1, nameof(StepperNav.ChildContent), (RenderFragment) (navBuilder =>
                {
                    navBuilder.OpenComponent<StepperItem>(0);
                    navBuilder.AddAttribute(1, nameof(StepperItem.Step), 1);
                    navBuilder.AddAttribute(2, nameof(StepperItem.ChildContent), (RenderFragment) (itemBuilder =>
                    {
                        itemBuilder.OpenComponent<StepperTrigger>(0);
                        itemBuilder.AddAttribute(1, nameof(StepperTrigger.AsChild), true);
                        itemBuilder.AddAttribute(2, nameof(StepperTrigger.ChildContent), (RenderFragment) (triggerBuilder =>
                        {
                            triggerBuilder.OpenComponent<StepperIndicator>(0);
                            triggerBuilder.AddAttribute(1, nameof(StepperIndicator.ChildContent), (RenderFragment) (indicatorBuilder => indicatorBuilder.AddContent(0, "1")));
                            triggerBuilder.CloseComponent();
                        }));
                        itemBuilder.CloseComponent();
                    }));
                    navBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        var trigger = cut.Find("[data-slot='stepper-trigger']");
        trigger.TagName.Should().Be("SPAN");
        trigger.GetAttribute("role").Should().BeNull();
        trigger.GetAttribute("aria-selected").Should().BeNull();
        trigger.GetAttribute("type").Should().BeNull();
        trigger.GetAttribute("data-state").Should().Be("active");
    }

    [Test]
    public void Stepper_supports_keyed_step_values()
    {
        var cut = Render<Stepper>(parameters => parameters
            .Add(p => p.DefaultValueKey, "details")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<StepperNav>(0);
                builder.AddAttribute(1, nameof(StepperNav.ChildContent), (RenderFragment) (navBuilder =>
                {
                    AddKeyedItem(navBuilder, 0, 1, "details");
                    AddKeyedItem(navBuilder, 10, 2, "review");
                }));
                builder.CloseComponent();
            }));

        var nav = cut.Find("[data-slot='stepper-nav']");
        nav.GetAttribute("data-state").Should().Be("details");

        var trigger = cut.Find("[data-slot='stepper-trigger']");
        trigger.GetAttribute("id").Should().Be("stepper-tab-details");
        trigger.GetAttribute("aria-controls").Should().Be("stepper-panel-details");
        trigger.GetAttribute("aria-selected").Should().Be("true");
    }

    [Test]
    public void Stepper_marks_prior_items_completed_when_keyed_value_is_not_first()
    {
        var cut = Render<Stepper>(parameters => parameters
            .Add(p => p.DefaultValueKey, "review")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<StepperNav>(0);
                builder.AddAttribute(1, nameof(StepperNav.ChildContent), (RenderFragment) (navBuilder =>
                {
                    AddKeyedItem(navBuilder, 0, 1, "details");
                    AddKeyedItem(navBuilder, 10, 2, "review");
                    AddKeyedItem(navBuilder, 20, 3, "done");
                }));
                builder.CloseComponent();
            }));

        cut.WaitForAssertion(() =>
        {
            var items = cut.FindAll("[data-slot='stepper-item']");
            items[0].GetAttribute("data-state").Should().Be("completed");
            items[1].GetAttribute("data-state").Should().Be("active");
            items[2].GetAttribute("data-state").Should().Be("inactive");
        });
    }

    [Test]
    public void Stepper_stacked_item_variant_applies_connector_layout_defaults()
    {
        var cut = Render<Stepper>(parameters => parameters
            .Add(p => p.DefaultValueKey, "confirm")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<StepperNav>(0);
                builder.AddAttribute(1, nameof(StepperNav.ChildContent), (RenderFragment) (navBuilder =>
                {
                    AddStackedItem(navBuilder, 0, 1, "details", "Details", "Create an account");
                    AddStackedItem(navBuilder, 10, 2, "confirm", "Confirm", "Review details");
                    AddStackedItem(navBuilder, 20, 3, "done", "Done", "Complete setup");
                }));
                builder.CloseComponent();
        }));

        var firstItem = cut.Find("[data-slot='stepper-item']");
        ShouldContainClasses(firstItem.GetAttribute("class"), "relative", "flex-1");

        var triggerClass = cut.Find("[data-slot='stepper-trigger']").GetAttribute("class");
        triggerClass.Should().Contain("flex-col");
        triggerClass.Should().NotContain("inline-flex");

        var separatorClass = cut.Find("[data-slot='stepper-separator']").GetAttribute("class");
        ShouldContainClasses(separatorClass, "absolute", "inset-x-0", "top-2", "right-[calc(-50%+18px)]", "left-[calc(50%+18px)]");

        ShouldContainClasses(cut.Find("[data-slot='stepper-title']").GetAttribute("class"), "text-sm", "font-medium");
        var descriptionClass = cut.Find("[data-slot='stepper-description']").GetAttribute("class");
        ShouldContainClasses(descriptionClass, "hidden", "md:block", "text-xs", "font-medium", "whitespace-nowrap");
    }

    private static void ShouldContainClasses(string? actual, params string[] expectedClasses)
    {
        actual.Should().NotBeNull();
        string[] actualClasses = actual!.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string expectedClass in expectedClasses)
        {
            actualClasses.Should().Contain(expectedClass);
        }
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

    private static void AddTemplatedIndicatorItem(RenderTreeBuilder builder, int sequence, int step, bool loading)
    {
        builder.OpenComponent<StepperItem>(sequence);
        builder.AddAttribute(sequence + 1, nameof(StepperItem.Step), step);
        builder.AddAttribute(sequence + 2, nameof(StepperItem.Loading), loading);
        builder.AddAttribute(sequence + 3, nameof(StepperItem.ChildContent), (RenderFragment) (itemBuilder =>
        {
            itemBuilder.OpenComponent<StepperTrigger>(0);
            itemBuilder.AddAttribute(1, nameof(StepperTrigger.ChildContent), (RenderFragment) (triggerBuilder =>
            {
                triggerBuilder.OpenComponent<StepperIndicator>(0);
                triggerBuilder.AddAttribute(1, nameof(StepperIndicator.ChildContent), (RenderFragment) (indicatorBuilder => indicatorBuilder.AddContent(0, step)));
                triggerBuilder.CloseComponent();
            }));
            itemBuilder.CloseComponent();
        }));
        builder.CloseComponent();
    }

    private static void AddStackedItem(RenderTreeBuilder builder, int sequence, int step, string value, string title, string description)
    {
        builder.OpenComponent<StepperItem>(sequence);
        builder.AddAttribute(sequence + 1, nameof(StepperItem.Step), step);
        builder.AddAttribute(sequence + 2, nameof(StepperItem.Value), value);
        builder.AddAttribute(sequence + 3, nameof(StepperItem.Variant), StepperItemVariant.Stacked);
        builder.AddAttribute(sequence + 4, nameof(StepperItem.ChildContent), (RenderFragment) (itemBuilder =>
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

            if (step < 3)
            {
                itemBuilder.OpenComponent<StepperSeparator>(2);
                itemBuilder.CloseComponent();
            }
        }));
        builder.CloseComponent();
    }

    private static void AddKeyedItem(RenderTreeBuilder builder, int sequence, int step, string value)
    {
        builder.OpenComponent<StepperItem>(sequence);
        builder.AddAttribute(sequence + 1, nameof(StepperItem.Step), step);
        builder.AddAttribute(sequence + 2, nameof(StepperItem.Value), value);
        builder.AddAttribute(sequence + 3, nameof(StepperItem.ChildContent), (RenderFragment) (itemBuilder =>
        {
            itemBuilder.OpenComponent<StepperTrigger>(0);
            itemBuilder.AddAttribute(1, nameof(StepperTrigger.ChildContent), (RenderFragment) (triggerBuilder =>
            {
                triggerBuilder.OpenComponent<StepperIndicator>(0);
                triggerBuilder.AddAttribute(1, nameof(StepperIndicator.ChildContent), (RenderFragment) (indicatorBuilder => indicatorBuilder.AddContent(0, step)));
                triggerBuilder.CloseComponent();
            }));
            itemBuilder.CloseComponent();
        }));
        builder.CloseComponent();
    }
}
