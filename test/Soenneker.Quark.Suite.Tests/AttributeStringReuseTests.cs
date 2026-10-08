using System;
using System.Collections.Generic;
using AwesomeAssertions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed class AttributeStringReuseTests
{
    [Test]
    public async ValueTask Event_boxes_are_reused_and_replaced_handlers_take_effect(CancellationToken cancellationToken)
    {
        var calls = new List<string>();
        var probe = new AttributeStringReuseProbe();
        probe.Handler = EventCallback.Factory.Create<string>(calls, value => calls.Add("first:" + value));
        object first = probe.BuildEvents()["onchange"];
        probe.BuildEvents()["onchange"].Should().BeSameAs(first);
        for (int i = 0; i < 100; i++) probe.BuildEvents();
        long before = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < 100; i++) probe.BuildEvents();
        (GC.GetAllocatedBytesForCurrentThread() - before).Should().Be(0);
        probe.Handler = EventCallback.Factory.Create<string>(calls, value => calls.Add("second:" + value));
        object second = probe.BuildEvents()["onchange"];
        second.Should().NotBeSameAs(first);
        probe.BuildEvents()["onchange"].Should().BeSameAs(second);
        await ((EventCallback<string>)second).InvokeAsync("value");
        calls.Should().Equal("second:value");
        probe.Handler = default;
        probe.BuildEvents().Should().NotContainKey("onchange");
    }

    [Test]
    public void Repeated_output_reuses_strings_without_steady_state_allocations()
    {
        var probe = new AttributeStringReuseProbe();
        var attributes = new Dictionary<string, object>();
        void Build()
        {
            attributes.Clear();
            attributes["class"] = "caller";
            attributes["style"] = "color: red";
            probe.Build(attributes, "component", "width: 1px;");
            probe.Prepend(attributes, "prefix");
        }

        for (int i = 0; i < 100; i++) Build();
        object classes = attributes["class"];
        object style = attributes["style"];
        long before = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < 100; i++) Build();
        long allocated = GC.GetAllocatedBytesForCurrentThread() - before;

        allocated.Should().Be(0);
        attributes["class"].Should().BeSameAs(classes);
        attributes["style"].Should().BeSameAs(style);
        classes.Should().Be("prefix component caller");
    }

    [Test]
    public void Changed_caller_and_component_values_replace_cached_output_and_empty_values_clear_it()
    {
        var probe = new AttributeStringReuseProbe();
        var attributes = new Dictionary<string, object> { ["class"] = "old", ["style"] = "color: red" };
        probe.Build(attributes, "component", "width: 1px;");
        attributes.Clear();
        attributes["class"] = "new";
        attributes["style"] = "color: blue";
        probe.Build(attributes, "changed", "width: 2px;");
        probe.Prepend(attributes, "prefix");
        attributes["class"].Should().Be("prefix changed new");
        attributes["style"].ToString().Should().Contain("width: 2px;").And.Contain("color: blue").And.NotContain("red");

        attributes.Clear();
        probe.Build(attributes, "", "");
        attributes.Should().BeEmpty();
        probe.Prepend(attributes, "fresh");
        attributes["class"].Should().Be("fresh");
    }
}
