using System.Linq;
using System.Collections.Generic;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    [Arguments(typeof(Anchor))]
    [Arguments(typeof(Label))]
    [Arguments(typeof(Span))]
    public void Slotted_typographic_elements_do_not_forward_ids_to_descendants(System.Type type)
    {
        RenderFragment child = builder =>
        {
            builder.OpenComponent(0, type);
            builder.AddAttribute(1, "ChildContent", (RenderFragment)(content =>
            {
                content.OpenComponent<Span>(0);
                content.AddAttribute(1, nameof(Span.ChildContent), (RenderFragment)(text => text.AddContent(0, "Label")));
                content.CloseComponent();
            }));
            builder.CloseComponent();
        };
        var cut = Render<Button>(p => p.Add(c => c.AsChild, true).Add(c => c.Id, "slot-root").Add(c => c.ChildContent, child));
        cut.FindAll("#slot-root").Should().HaveCount(1);
    }

    [Test]
    public void Slotted_button_does_not_duplicate_its_id_or_handlers_on_nested_spans()
    {
        int calls = 0;
        var cut = Render<Button>(p => p.Add(c => c.AsChild, true).Add(c => c.Id, "outer-button")
            .Add(c => c.OnClick, () => calls++)
            .AddChildContent<Button>(button => button.AddChildContent<Span>(span => span.AddChildContent<Span>(nested => nested.AddChildContent("Label")))));
        cut.FindAll("#outer-button").Should().HaveCount(1);
        cut.FindAll("span").Last().Click();
        calls.Should().Be(1);
    }

    [Test]
    public void Sidebar_slot_reuses_its_callback_box_and_invokes_replaced_handlers()
    {
        var calls = new List<string>();
        RenderFragment child = builder =>
        {
            builder.OpenComponent<Span>(0);
            builder.AddAttribute(1, nameof(Span.ChildContent), (RenderFragment)(content => content.AddContent(0, "Action")));
            builder.CloseComponent();
        };
        var cut = Render<SidebarGroupAction>(p => p
            .Add(c => c.AsChild, true)
            .Add(c => c.ChildContent, child)
            .Add(c => c.OnClick, EventCallback.Factory.Create<MouseEventArgs>(calls, () => calls.Add("first"))));
        object first = cut.FindComponent<CascadingValue<IReadOnlyDictionary<string, object>>>().Instance.Value!["onclick"];
        cut.Find("span").Click();
        cut.Render(p => p.Add(c => c.OnClick, EventCallback.Factory.Create<MouseEventArgs>(calls, () => calls.Add("replacement"))));
        cut.FindComponent<CascadingValue<IReadOnlyDictionary<string, object>>>().Instance.Value!["onclick"].Should().BeSameAs(first);
        cut.Find("span").Click();
        calls.Should().Equal("first", "replacement");
        cut.Render(p => p.Add(c => c.OnClick, default(EventCallback<MouseEventArgs>)));
        cut.Find("span").Click();
        calls.Should().HaveCount(2);
    }
}
