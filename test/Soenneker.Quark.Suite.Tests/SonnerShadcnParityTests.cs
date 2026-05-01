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
            toaster.GetAttribute("style")!.Should().Contain("--normal-bg: var(--popover)");
            toaster.GetAttribute("style")!.Should().Contain("--normal-text: var(--popover-foreground)");
            toaster.GetAttribute("style")!.Should().Contain("--normal-border: var(--border)");

            toast.GetAttribute("class").Should().Be("cn-toast");
            toast.GetAttribute("data-type").Should().Be("success");
            toast.GetAttribute("data-styled").Should().Be("true");
            toast.GetAttribute("data-rich-colors").Should().Be("false");
            toast.TextContent.Should().Contain("Saved");
        });
    }

    [Test]
    public async Task Sonner_omits_data_type_for_default_toasts_like_shadcn()
    {
        var cut = Render<Sonner>();
        var service = Services.GetRequiredService<ISonnerService>();

        await service.Toast("Event has been created");

        cut.WaitForAssertion(() =>
        {
            var toast = cut.Find("li[data-sonner-toast]");

            toast.HasAttribute("data-type").Should().BeFalse();
        });
    }

    [Test]
    public void Sonner_interop_uses_dom_rect_height_like_upstream_sonner()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForSonner(), "src", "Soenneker.Quark.Suite", "wwwroot", "js", "sonnerinterop.js"));

        source.Should().Contain("toast.getBoundingClientRect().height");
        source.Should().Contain("value instanceof Element");
        source.Should().Contain("return true;");
        source.Should().Contain("return false;");
        source.Should().NotContain("heights[id] = toast.offsetHeight");
    }

    [Test]
    public void Sonner_css_maps_buttons_to_shadcn_toast_options()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForSonner(), "src", "Soenneker.Quark.Suite", "wwwroot", "css", "sonner.css"));

        source.Should().Contain("background: var(--primary)");
        source.Should().Contain("color: var(--primary-foreground)");
        source.Should().Contain("background: var(--muted)");
        source.Should().Contain("color: var(--muted-foreground)");
        source.Should().NotContain("background: var(--normal-text)");
        source.Should().NotContain("color: var(--normal-bg)");
    }

    [Test]
    public void Sonner_demo_mounts_toaster_for_live_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForSonner(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Sonner.razor"));

        source.Should().Contain("<Soenneker.Quark.Sonner />");
        source.IndexOf("<Soenneker.Quark.Sonner />", StringComparison.Ordinal).Should().BeLessThan(source.IndexOf("<Div Space=\"Space.Y.Is8\"", StringComparison.Ordinal));
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
