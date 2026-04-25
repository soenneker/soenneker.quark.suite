using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Steps_emit_accessible_tablist_tab_and_panel_contract()
    {
        var cut = Render<Steps>(parameters => parameters
            .Add(p => p.SelectedStep, "details")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Step>(0);
                builder.AddAttribute(1, nameof(Step.Name), "details");
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(stepBuilder => stepBuilder.AddContent(0, "Details")));
                builder.CloseComponent();

                builder.OpenComponent<Step>(3);
                builder.AddAttribute(4, nameof(Step.Name), "billing");
                builder.AddAttribute(5, nameof(Step.Disabled), true);
                builder.AddAttribute(6, "ChildContent", (RenderFragment)(stepBuilder => stepBuilder.AddContent(0, "Billing")));
                builder.CloseComponent();

                builder.OpenComponent<StepContent>(7);
                builder.AddAttribute(8, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<StepPanel>(0);
                    contentBuilder.AddAttribute(1, nameof(StepPanel.Name), "details");
                    contentBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(panelBuilder => panelBuilder.AddContent(0, "Details panel")));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<StepPanel>(3);
                    contentBuilder.AddAttribute(4, nameof(StepPanel.Name), "billing");
                    contentBuilder.AddAttribute(5, "ChildContent", (RenderFragment)(panelBuilder => panelBuilder.AddContent(0, "Billing panel")));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var tablist = cut.Find("[data-slot='steps']");
        var content = cut.Find("[data-slot='step-content']");
        var tabs = cut.FindAll("[role='tab']");
        var panels = cut.FindAll("[role='tabpanel']");

        tablist.GetAttribute("role").Should().Be("tablist");
        tablist.GetAttribute("class")!.Should().Contain("flex-wrap");
        tablist.GetAttribute("class")!.Should().Contain("list-none");
        content.GetAttribute("class")!.Should().Contain("mt-4");

        tabs[0].GetAttribute("aria-selected").Should().Be("true");
        tabs[0].GetAttribute("aria-current").Should().Be("step");
        tabs[0].GetAttribute("aria-controls").Should().EndWith("panel-details");
        tabs[0].GetAttribute("aria-posinset").Should().Be("1");
        tabs[0].GetAttribute("aria-setsize").Should().Be("2");
        tabs[0].GetAttribute("tabindex").Should().Be("0");

        tabs[1].GetAttribute("aria-disabled").Should().Be("true");
        tabs[1].GetAttribute("href").Should().BeNull();
        tabs[1].GetAttribute("tabindex").Should().Be("-1");

        panels[0].GetAttribute("aria-labelledby").Should().EndWith("tab-details");
        panels[0].GetAttribute("class")!.Should().NotContain("hidden");
        panels[1].GetAttribute("class")!.Should().Contain("hidden");
        panels[1].GetAttribute("tabindex").Should().Be("-1");
    }
}
