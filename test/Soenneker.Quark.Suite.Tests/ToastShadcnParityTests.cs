using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public async Task Toaster_matches_shadcn_toast_demo_contract()
    {
        var cut = Render<Toaster>();
        var service = Services.GetRequiredService<IToastUtil>();

        await service.Toast("Scheduled: Catch up", options =>
        {
            options.Description = "Friday, February 10, 2023 at 5:57 PM";
            options.ActionLabel = "Undo";
            options.ActionAltText = "Undo";
        });

        cut.WaitForAssertion(() =>
        {
            var viewport = cut.Find("[data-slot='toast-viewport']");
            var toast = cut.Find("[data-slot='toast']");
            var title = cut.Find("[data-slot='toast-title']");
            var description = cut.Find("[data-slot='toast-description']");
            var action = cut.Find("[data-slot='toast-action']");
            var close = cut.Find("[data-slot='toast-close']");

            cut.FindAll("[data-slot='toast']").Should().HaveCount(1);
            viewport.GetAttribute("class").Should().Contain("fixed");
            viewport.GetAttribute("class").Should().Contain("z-[100]");
            viewport.GetAttribute("class").Should().Contain("flex-col-reverse");
            viewport.GetAttribute("class").Should().Contain("md:max-w-[420px]");

            toast.GetAttribute("class").Should().Contain("group pointer-events-auto relative flex w-full items-center justify-between");
            toast.GetAttribute("class").Should().Contain("rounded-md border p-4 pr-6 shadow-lg");
            toast.GetAttribute("class").Should().Contain("data-[state=open]:animate-in");
            toast.GetAttribute("class").Should().Contain("border bg-background text-foreground");
            toast.GetAttribute("data-state").Should().Be("open");
            toast.GetAttribute("data-radix-toast-root").Should().NotBeNull();

            title.GetAttribute("class").Should().Contain("text-sm font-semibold [&+div]:text-xs");
            description.GetAttribute("class").Should().Contain("text-sm opacity-90");
            action.GetAttribute("class").Should().Contain("inline-flex h-8 shrink-0 items-center justify-center rounded-md border bg-transparent px-3 text-sm font-medium");
            action.GetAttribute("data-radix-toast-announce-alt").Should().Be("Undo");
            close.GetAttribute("class").Should().Contain("absolute right-1 top-1 rounded-md p-1 text-foreground/50 opacity-0");
            close.GetAttribute("toast-close").Should().Be(string.Empty);

            cut.Markup.Should().Contain("Scheduled: Catch up");
            cut.Markup.Should().Contain("Friday, February 10, 2023 at 5:57 PM");
            cut.Markup.Should().Contain("Undo");
        });
    }

    [Test]
    public async Task Toast_destructive_variant_matches_shadcn_variant_classes()
    {
        var cut = Render<Toaster>();
        var service = Services.GetRequiredService<IToastUtil>();

        await service.Toast("Delete", options => options.Variant = ToastVariant.Destructive);

        cut.WaitForAssertion(() =>
        {
            var toast = cut.Find("[data-slot='toast']");
            toast.GetAttribute("class").Should().Contain("destructive group border-destructive bg-destructive text-destructive-foreground");
        });
    }
}
