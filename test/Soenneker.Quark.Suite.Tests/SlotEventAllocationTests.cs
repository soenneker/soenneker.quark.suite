using System.Collections.Generic;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
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
