using System.Collections.Generic;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Numeric_validation_selector_reuses_its_delegate_and_reads_current_value()
    {
        var cut = Render<NumericInput>(p => p.Add(c => c.Value, 1m));
        var selector = cut.FindComponent<Input>().Instance.ValidationValueSelector!;
        selector().Should().Be(1m);
        cut.Render(p => p.Add(c => c.Value, 2m));
        cut.FindComponent<Input>().Instance.ValidationValueSelector.Should().BeSameAs(selector);
        selector().Should().Be(2m);
        cut.Render(p => p.Add(c => c.Value, (decimal?)null));
        selector().Should().BeNull();
    }

    [Test]
    public void Password_validation_selector_and_wrapper_follow_parameter_changes()
    {
        var cut = Render<PasswordInput>(p => p.Add(c => c.Value, "first").Add(c => c.WrapperClass, "custom-one"));
        var selector = cut.FindComponent<Input>().Instance.ValidationValueSelector!;
        selector().Should().Be("first");
        cut.Render(p => p.Add(c => c.Value, "second").Add(c => c.WrapperClass, "custom-two"));
        cut.FindComponent<Input>().Instance.ValidationValueSelector.Should().BeSameAs(selector);
        selector().Should().Be("second");
        var wrapper = cut.Find("[data-slot='password-input']");
        wrapper.ClassList.Should().Contain("custom-two").And.NotContain("custom-one");
        cut.Render(p => p.Add(c => c.WrapperClass, (string?)null));
        cut.Find("[data-slot='password-input']").ClassList.Should().Contain("inline-flex").And.NotContain("custom-two");
    }

    [Test]
    public void Numeric_input_updates_and_removes_buffered_constraints()
    {
        var cut = Render<NumericInput>(p => p.Add(c => c.Min, 1m).Add(c => c.Max, 10m).Add(c => c.Step, 2m));
        cut.Find("input").GetAttribute("min").Should().Be("1");
        cut.Render(p => p.Add(c => c.Min, 3m).Add(c => c.Max, 20m));
        cut.Find("input").GetAttribute("min").Should().Be("3");
        cut.Find("input").GetAttribute("max").Should().Be("20");
        cut.Render(p => p.Add(c => c.Min, (decimal?)null).Add(c => c.Max, (decimal?)null).Add(c => c.Step, (decimal?)null));
        cut.Find("input").HasAttribute("min").Should().BeFalse();
        cut.Find("input").HasAttribute("max").Should().BeFalse();
        cut.Find("input").HasAttribute("step").Should().BeFalse();
    }

    [Test]
    public void Password_input_updates_and_removes_buffered_input_attributes()
    {
        var cut = Render<PasswordInput>(p => p.Add(c => c.MaxLength, 10).Add(c => c.AutoComplete, "current-password"));
        cut.Find("input").GetAttribute("maxlength").Should().Be("10");
        cut.Render(p => p.Add(c => c.MaxLength, 20).Add(c => c.AutoComplete, "new-password"));
        cut.Find("input").GetAttribute("maxlength").Should().Be("20");
        cut.Render(p => p.Add(c => c.MaxLength, 0).Add(c => c.AutoComplete, (string?)null));
        cut.Find("input").HasAttribute("maxlength").Should().BeFalse();
        cut.Find("input").HasAttribute("autocomplete").Should().BeFalse();
    }

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
