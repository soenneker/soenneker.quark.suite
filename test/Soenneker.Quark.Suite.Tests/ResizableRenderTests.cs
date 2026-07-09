using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Resizable_handle_only_registers_when_its_interop_configuration_changes()
    {
        RenderFragment content = builder =>
        {
            builder.OpenComponent<ResizableHandle>(0);
            builder.CloseComponent();
        };

        var cut = Render<ResizablePanelGroup>(parameters => parameters
            .Add(component => component.Orientation, Orientation.Horizontal)
            .Add(component => component.ChildContent, content));
        var interop = (FakeResizableInterop) Services.GetRequiredService<IResizableInterop>();

        interop.RegisterHandleCallCount.Should().Be(1);

        await cut.InvokeAsync(cut.Instance.Refresh);
        interop.RegisterHandleCallCount.Should().Be(1);

        cut.Render(parameters => parameters
            .Add(component => component.Orientation, Orientation.Vertical)
            .Add(component => component.ChildContent, content));
        interop.RegisterHandleCallCount.Should().Be(2);
    }
}
