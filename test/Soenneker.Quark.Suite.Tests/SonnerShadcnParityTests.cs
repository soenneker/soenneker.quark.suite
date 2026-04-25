using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
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

    [Test]
    public void Sonner_interop_uses_dom_rect_height_like_upstream_sonner()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForSonner(), "src", "Soenneker.Quark.Suite", "wwwroot", "js", "sonnerinterop.js"));

        source.Should().Contain("toast.getBoundingClientRect().height");
        source.Should().NotContain("heights[id] = toast.offsetHeight");
    }

    private static string GetSuiteRootForSonner()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
