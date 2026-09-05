using AwesomeAssertions;
using Bunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class RenderOptimizationTests : BunitContext
{
    public RenderOptimizationTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public async Task Content_refresh_preserves_attributes_but_parameter_changes_still_invalidate_them()
    {
        var attrs = new Dictionary<string, object> { ["data-test"] = "before" };
        var cut = Render<AttributeCacheProbe>(p => p.Add(c => c.Attributes, attrs));
        int builds = cut.Instance.AttributeBuilds;
        string classString = cut.Instance.LastClass!;
        await cut.InvokeAsync(() => cut.Instance.SetContent("updated"));
        cut.Instance.AttributeBuilds.Should().Be(builds);
        cut.Markup.Should().Contain("updated");
        attrs["data-test"] = "after";
        cut.Render(p => p.Add(c => c.Attributes, attrs));
        cut.Find("div").GetAttribute("data-test").Should().Be("after");
        ReferenceEquals(classString, cut.Instance.LastClass).Should().BeTrue();
        cut.Render(p => p.Add(c => c.VisualClass, "changed"));
        cut.Find("div").GetAttribute("class").Should().Be("changed");
        attrs.Remove("data-test");
        cut.Render(p => p.Add(c => c.Attributes, attrs));
        cut.Find("div").HasAttribute("data-test").Should().BeFalse();
    }

    [Test]
    public void Default_suppression_renders_when_an_untracked_parameter_reference_changes()
    {
        var model = new RenderProbeModel { Value = 1 };
        var cut = Render<MutableParameterProbe>(parameters => parameters.Add(component => component.Model, model));

        cut.Render(parameters => parameters.Add(component => component.Model, new RenderProbeModel { Value = 2 }));

        cut.Markup.Should().Contain(">2<");
    }

    [Test]
    public void AlwaysRender_observes_in_place_parameter_mutation()
    {
        Services.AddSingleton(new QuarkOptions { AlwaysRender = true });
        var model = new RenderProbeModel { Value = 1 };
        var cut = Render<MutableParameterProbe>(parameters => parameters.Add(component => component.Model, model));

        model.Value = 2;
        cut.Render(parameters => parameters.Add(component => component.Model, model));

        cut.Markup.Should().Contain(">2<");
    }

    [Test]
    public async Task Default_rendering_observes_internal_event_state_after_parent_rerender()
    {
        var cut = Render<InteractiveRenderProbe>(parameters => parameters.Add(component => component.Label, "Count"));

        cut.Render(parameters => parameters.Add(component => component.Label, "Count"));
        await cut.Find("button").ClickAsync(new MouseEventArgs());

        cut.Markup.Should().Contain("Count: 1");
    }

    [Test]
    public void Default_render_suppression_skips_an_unchanged_complex_parameter()
    {
        RenderCountingProbe.RenderCount = 0;
        var model = new RenderProbeModel { Value = 1 };
        var cut = Render<RenderCountingProbe>(parameters => parameters.Add(component => component.Model, model));

        cut.Render(parameters => parameters.Add(component => component.Model, model));

        RenderCountingProbe.RenderCount.Should().Be(1);
    }

    [Test]
    public void Default_parameters_are_applied_once_per_render_generation()
    {
        DefaultApplicationProbe.ApplicationCount = 0;
        var cut = Render<DefaultApplicationProbe>(parameters => parameters.Add(component => component.Value, 1));

        DefaultApplicationProbe.ApplicationCount.Should().Be(1);

        cut.Render(parameters => parameters.Add(component => component.Value, 2));

        DefaultApplicationProbe.ApplicationCount.Should().Be(2);
    }

    [Test]
    public void Immutable_cascading_parameters_use_the_unchanged_fast_path()
    {
        CascadeKeyProbe.KeyCount = 0;
        var payload = new RenderProbeModel { Value = 1 };
        var cut = Render<CascadeProbeHost>(parameters => parameters
            .Add(component => component.Payload, payload)
            .Add(component => component.Tick, 0));

        var initialKeyCount = CascadeKeyProbe.KeyCount;
        cut.Render(parameters => parameters
            .Add(component => component.Payload, payload)
            .Add(component => component.Tick, 1));

        CascadeKeyProbe.KeyCount.Should().Be(initialKeyCount);
    }
}

public sealed class RenderProbeModel
{
    public int Value { get; set; }
}

public sealed class MutableParameterProbe : Element
{
    [Parameter, EditorRequired]
    public RenderProbeModel Model { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "span");
        builder.AddContent(1, Model.Value);
        builder.CloseElement();
    }
}

public sealed class InteractiveRenderProbe : Element
{
    private int _count;

    [Parameter]
    public string Label { get; set; } = "Count";

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "button");
        builder.AddAttribute(1, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, () => _count++));
        builder.AddContent(2, $"{Label}: {_count}");
        builder.CloseElement();
    }
}

public sealed class RenderCountingProbe : Element
{
    public static int RenderCount { get; set; }

    [Parameter, EditorRequired]
    public RenderProbeModel Model { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        RenderCount++;
        builder.OpenElement(0, "span");
        builder.AddContent(1, Model.Value);
        builder.CloseElement();
    }

    protected override void ComputeRenderKeyCore(ref HashCode hashCode)
    {
        base.ComputeRenderKeyCore(ref hashCode);
        hashCode.Add(Model);
    }
}

public sealed class DefaultApplicationProbe : Element
{
    public static int ApplicationCount { get; set; }

    [Parameter]
    public int Value { get; set; }

    protected override void ApplyDefaultParameters()
    {
        base.ApplyDefaultParameters();
        ApplicationCount++;
        DataSlot ??= "default-application-probe";
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "span");
        builder.AddMultipleAttributes(1, BuildAttributes());
        builder.AddContent(2, Value);
        builder.CloseElement();
    }
}

public sealed class CascadeProbeHost : ComponentBase
{
    [Parameter]
    public RenderProbeModel Payload { get; set; } = null!;

    [Parameter]
    public int Tick { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<int>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<int>.Value), 42);
        builder.AddAttribute(2, nameof(CascadingValue<int>.ChildContent), (RenderFragment)(contentBuilder =>
        {
            contentBuilder.OpenComponent<CascadeKeyProbe>(0);
            contentBuilder.AddAttribute(1, nameof(CascadeKeyProbe.Payload), Payload);
            contentBuilder.CloseComponent();
        }));
        builder.CloseComponent();
        builder.AddContent(3, Tick);
    }
}

public sealed class CascadeKeyProbe : Element
{
    public static int KeyCount { get; set; }

    [CascadingParameter]
    public int CascadeValue { get; set; }

    [Parameter]
    public RenderProbeModel Payload { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "span");
        builder.AddContent(1, CascadeValue + Payload.Value);
        builder.CloseElement();
    }

    protected override void ComputeRenderKeyCore(ref HashCode hashCode)
    {
        KeyCount++;
        base.ComputeRenderKeyCore(ref hashCode);
        hashCode.Add(CascadeValue);
        hashCode.Add(Payload);
    }
}

public sealed class AttributeCacheProbe : RenderComponent
{
    [Parameter] public string VisualClass { get; set; } = "initial";
    public int AttributeBuilds { get; private set; }
    public string? LastClass { get; private set; }
    private string _content = "initial";
    protected override bool AlwaysRender => false;
    public void SetContent(string content)
    {
        _content = content;
        RequestContentRender();
    }
    protected override void BuildOwnedClassAndStyle(ref Soenneker.Utils.PooledStringBuilders.PooledStringBuilder style,
        ref Soenneker.Utils.PooledStringBuilders.PooledStringBuilder classes) => AppendClass(ref classes, VisualClass);
    protected override void BuildAttributesCore(Dictionary<string, object> attrs)
    {
        AttributeBuilds++;
        LastClass = (string)attrs["class"];
    }
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddMultipleAttributes(1, BuildAttributes());
        builder.AddContent(2, _content);
        builder.CloseElement();
    }
}
