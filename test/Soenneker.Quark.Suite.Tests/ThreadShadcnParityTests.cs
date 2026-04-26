using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Thread_components_use_nexus_slots()
    {
        var cut = Render<Thread>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<ThreadContent>(0);
                builder.AddAttribute(1, nameof(ThreadContent.ChildContent), (RenderFragment)(content => content.AddContent(0, "Message")));
                builder.CloseComponent();

                builder.OpenComponent<ThreadScrollToBottom>(2);
                builder.AddAttribute(3, nameof(ThreadScrollToBottom.HideWhenAtBottom), false);
                builder.CloseComponent();
            })));

        cut.Find("[data-slot='thread']").Should().NotBeNull();
        cut.Find("[data-slot='thread-content']").TextContent.Should().Contain("Message");
        cut.Find("[data-slot='thread-scroll-to-bottom']").GetAttribute("type").Should().Be("button");
    }
}
