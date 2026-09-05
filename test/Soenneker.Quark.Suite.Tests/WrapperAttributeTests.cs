using System.Collections.Generic;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Performance_text_input_removes_old_wrapper_attributes()
    {
        var attributes = new Dictionary<string, object> { ["data-test"] = "before" };
        var cut = Render<TextInput>(p => p.Add(c => c.MaxLength, 10).Add(c => c.AdditionalAttributes, attributes));
        cut.Find("input").GetAttribute("maxlength").Should().Be("10");
        attributes["data-test"] = "after";
        cut.Render(p => p.Add(c => c.MaxLength, 20).Add(c => c.AdditionalAttributes, attributes));
        cut.Find("input").GetAttribute("maxlength").Should().Be("20");
        cut.Find("input").GetAttribute("data-test").Should().Be("after");
        attributes.Clear();
        cut.Render(p => p.Add(c => c.MaxLength, 0).Add(c => c.AdditionalAttributes, attributes));
        cut.Find("input").HasAttribute("maxlength").Should().BeFalse();
        cut.Find("input").HasAttribute("data-test").Should().BeFalse();
    }

    [Test]
    public void Performance_button_slot_preserves_previous_bag_and_removes_old_attributes()
    {
        var attributes = new Dictionary<string, object> { ["data-test"] = "before" };
        var cut = Render<Button>(p => p.Add(c => c.AsChild, true).Add(c => c.Attributes, attributes)
            .AddChildContent<PerformanceSlotProbe>());
        var previous = cut.FindComponent<PerformanceSlotProbe>().Instance.Attributes!;
        attributes["data-test"] = "after";
        cut.Render(p => p.Add(c => c.Attributes, attributes));
        previous["data-test"].Should().Be("before");
        cut.Find("a").GetAttribute("data-test").Should().Be("after");
        cut.Find("a").HasAttribute("type").Should().BeFalse();
        attributes.Clear();
        cut.Render(p => p.Add(c => c.Attributes, attributes));
        cut.Find("a").HasAttribute("data-test").Should().BeFalse();
    }
}

public sealed class PerformanceSlotProbe : ComponentBase
{
    [CascadingParameter(Name = "SlotAttributes")]
    public IReadOnlyDictionary<string, object>? Attributes { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "a");
        builder.AddMultipleAttributes(1, Attributes);
        builder.CloseElement();
    }
}
