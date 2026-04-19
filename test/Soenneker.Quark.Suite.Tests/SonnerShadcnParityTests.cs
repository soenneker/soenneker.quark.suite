using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public async Task Sonner_matches_shadcn_toaster_shell()
    {
        var cut = Render<Sonner>();
        var service = Services.GetRequiredService<ISonnerService>();

        await service.Success("Saved");

        cut.WaitForAssertion(() =>
        {
            var section = cut.Find("section[aria-live='polite']");
            var toaster = cut.Find("ol[data-sonner-toaster='true']");
            var toast = cut.Find("li[data-sonner-toast]");

            section.GetAttribute("aria-label").Should().Be("Notifications alt+T");

            toaster.GetAttribute("class").Should().Be("toaster group");
            toaster.GetAttribute("data-y-position").Should().Be("top");
            toaster.GetAttribute("data-x-position").Should().Be("center");

            toast.GetAttribute("class").Should().Be("cn-toast");
            toast.GetAttribute("data-type").Should().Be("success");
            toast.GetAttribute("data-styled").Should().Be("true");
            toast.GetAttribute("data-rich-colors").Should().Be("false");
            toast.TextContent.Should().Contain("Saved");
        });
    }
}
