using System.Threading.Tasks;
using System.Globalization;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Lucide.Enums.Icons;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async ValueTask Sonner_styles_use_CSS_decimal_points_under_comma_decimal_cultures(CancellationToken cancellationToken)
    {
        var originalCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");
            var cut = Render<Sonner>(parameters => parameters.Add(p => p.ToastDuration, 0));
            var service = Services.GetRequiredService<ISonnerService>();
            await cut.InvokeAsync(async () =>
            {
                await service.Toast("First", cancellationToken: cancellationToken);
                await service.Toast("Second", cancellationToken: cancellationToken);
            });
            cut.WaitForAssertion(() =>
            {
                cut.Find("ol").GetAttribute("style").Should().Contain("--front-toast-height: 73.3px;");
                cut.Find("[data-index='1']").GetAttribute("style").Should().Contain("--offset: 87.3px;");
            });
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [Test]
    public async ValueTask Sonner_cancelled_pointer_and_lost_capture_clear_interaction_state(CancellationToken cancellationToken)
    {
        var cut = Render<Sonner>(parameters => parameters.Add(p => p.ToastDuration, 0));
        var service = Services.GetRequiredService<ISonnerService>();
        await cut.InvokeAsync(async () => await service.Toast("Notification", cancellationToken: cancellationToken));
        cut.WaitForAssertion(() => cut.Find("[data-sonner-toast]").GetAttribute("data-mounted").Should().Be("true"));
        cut.WaitForAssertion(() => cut.Find("[data-sonner-toast]").GetAttribute("data-expanded").Should().Be("false"));

        foreach (var releaseEvent in new[] { "onpointercancel", "onlostpointercapture", "onpointerup" })
        {
            await cut.Find("ol").TriggerEventAsync("onpointerdown", new PointerEventArgs { PointerId = 1 });
            cut.Find("[data-sonner-toast]").GetAttribute("data-expanded").Should().Be("true");
            await cut.Find("ol").TriggerEventAsync(releaseEvent, new PointerEventArgs { PointerId = 1 });
            cut.Find("[data-sonner-toast]").GetAttribute("data-expanded").Should().Be("false");
        }
    }

    [Test]
    public async ValueTask Sonner_reused_toast_updates_its_icon_and_loading_animation(CancellationToken cancellationToken)
    {
        var cut = Render<Sonner>(parameters => parameters.Add(p => p.ToastDuration, 0));
        var service = Services.GetRequiredService<ISonnerService>();
        await cut.InvokeAsync(async () => await service.Success("Saved", options => options.Id = "same-toast", cancellationToken: cancellationToken));
        cut.WaitForAssertion(() => cut.FindComponent<Icon>().Instance.Name.Should().Be(LucideIcon.CircleCheck));
        await cut.InvokeAsync(async () => await service.Info("Info", options => options.Id = "same-toast", cancellationToken: cancellationToken));
        cut.WaitForAssertion(() => cut.FindComponent<Icon>().Instance.Name.Should().Be(LucideIcon.Info));
        await cut.InvokeAsync(async () => await service.Warning("Warning", options => options.Id = "same-toast", cancellationToken: cancellationToken));
        cut.WaitForAssertion(() => cut.FindComponent<Icon>().Instance.Name.Should().Be(LucideIcon.TriangleAlert));
        await cut.InvokeAsync(async () => await service.Error("Error", options => options.Id = "same-toast", cancellationToken: cancellationToken));
        cut.WaitForAssertion(() => cut.FindComponent<Icon>().Instance.Name.Should().Be(LucideIcon.OctagonX));
        await cut.InvokeAsync(async () => await service.Loading("Loading", options => options.Id = "same-toast", cancellationToken: cancellationToken));
        cut.WaitForAssertion(() =>
        {
            cut.FindComponent<Spinner>().Instance.Name.Should().BeNull();
            cut.FindAll(".quark-spinner-circle").Should().HaveCount(3);
        });
        await cut.InvokeAsync(async () => await service.Toast("Done", options => options.Id = "same-toast", cancellationToken: cancellationToken));
        cut.WaitForAssertion(() => cut.FindComponents<Icon>().Should().BeEmpty());
        cut.FindAll("[data-sonner-toast]").Count.Should().Be(1);
    }
}
