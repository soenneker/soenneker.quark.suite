using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Grouped_radio_does_not_emit_conflicting_checked_state_attributes()
    {
        var cut = Render<RadioGroup>(parameters => parameters
            .Add(p => p.Value, "vm")
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<Radio>(0);
                builder.AddAttribute(1, nameof(Radio.Value), "kubernetes");
                builder.AddAttribute(2, nameof(Radio.ChildContent), (RenderFragment) (child => child.AddContent(0, "Kubernetes")));
                builder.CloseComponent();

                builder.OpenComponent<Radio>(3);
                builder.AddAttribute(4, nameof(Radio.Value), "vm");
                builder.AddAttribute(5, nameof(Radio.ChildContent), (RenderFragment) (child => child.AddContent(0, "Virtual Machine")));
                builder.CloseComponent();
            }));

        var radios = cut.FindAll("[role='radio']");

        radios.Should().HaveCount(2);

        radios[0].GetAttribute("aria-checked").Should().Be("false");
        radios[0].GetAttribute("data-state").Should().Be("unchecked");
        radios[0].HasAttribute("data-checked").Should().BeFalse();
        radios[0].HasAttribute("data-unchecked").Should().BeFalse();

        radios[1].GetAttribute("aria-checked").Should().Be("true");
        radios[1].GetAttribute("data-state").Should().Be("checked");
        radios[1].HasAttribute("data-checked").Should().BeFalse();
        radios[1].HasAttribute("data-unchecked").Should().BeFalse();
    }
}
