using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Alert_matches_shadcn_base_classes()
    {
        var alert = Render<Alert>(parameters => parameters
            .Add(p => p.ShowDefaultIcon, true)
            .Add(p => p.ChildContent, "Alert"));

        var alertElement = alert.Find("[data-slot='alert']");
        var alertClasses = alertElement.GetAttribute("class")!;

        alertClasses.Should().Contain("relative");
        alertClasses.Should().Contain("grid");
        alertClasses.Should().Contain("w-full");
        alertClasses.Should().Contain("group/alert");
        alertClasses.Should().Contain("gap-0.5");
        alertClasses.Should().Contain("rounded-lg");
        alertClasses.Should().Contain("border");
        alertClasses.Should().Contain("px-2.5");
        alertClasses.Should().Contain("py-2");
        alertClasses.Should().Contain("text-left");
        alertClasses.Should().Contain("text-sm");
        alertClasses.Should().Contain("bg-card");
        alertClasses.Should().Contain("text-card-foreground");
        alertClasses.Should().Contain("has-data-[slot=alert-action]:relative");
        alertClasses.Should().Contain("has-data-[slot=alert-action]:pr-18");
        alertClasses.Should().Contain("has-[>svg]:grid-cols-[auto_1fr]");
        alertClasses.Should().Contain("has-[>svg]:gap-x-2");
        alertClasses.Should().Contain("*:[svg]:row-span-2");
        alertClasses.Should().Contain("*:[svg]:translate-y-0.5");
        alertClasses.Should().Contain("*:[svg]:text-current");
        alertClasses.Should().Contain("*:[svg:not([class*='size-'])]:size-4");
        alertClasses.Should().NotContain("grid-cols-[0_1fr]");
        alertClasses.Should().NotContain("gap-y-0.5");
        alertClasses.Should().NotContain("px-4");
        alertClasses.Should().NotContain("py-3");
        alertClasses.Should().NotContain("has-[>svg]:grid-cols-[calc(var(--spacing)*4)_1fr]");
        alertClasses.Should().NotContain("[&>svg]:size-4");
        alertClasses.Should().NotContain("q-alert");
        (alertElement.GetAttribute("style") ?? "").Should().NotContain("border-color");
    }

    [Test]
    public void Alert_destructive_matches_shadcn_variant_classes_without_inline_color_styles()
    {
        var alert = Render<Alert>(parameters => parameters
            .Add(p => p.Variant, AlertVariant.Destructive)
            .Add(p => p.ShowDefaultIcon, true)
            .Add(p => p.ChildContent, "Alert"));

        var alertElement = alert.Find("[data-slot='alert']");
        var alertClasses = alertElement.GetAttribute("class")!;

        alertClasses.Should().Contain("bg-card");
        alertClasses.Should().Contain("text-destructive");
        alertClasses.Should().Contain("*:data-[slot=alert-description]:text-destructive/90");
        alertClasses.Should().Contain("*:[svg]:text-current");
        alertClasses.Should().NotContain("bg-alert-danger-bg");
        alertClasses.Should().NotContain("border-alert-danger");
        alertClasses.Should().NotContain("[&>svg]:text-alert-danger");
        (alertElement.GetAttribute("style") ?? "").Should().NotContain("border-color");
        (alertElement.GetAttribute("style") ?? "").Should().NotContain("color: white");
    }
}

