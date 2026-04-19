using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Alert_matches_shadcn_base_classes()
    {
        var alert = Render<Alert>(parameters => parameters
            .Add(p => p.ShowDefaultIcon, true)
            .Add(p => p.ChildContent, "Alert"));

        string alertClasses = alert.Find("[data-slot='alert']").GetAttribute("class")!;

        alertClasses.Should().Contain("group/alert");
        alertClasses.Should().Contain("relative");
        alertClasses.Should().Contain("grid");
        alertClasses.Should().Contain("w-full");
        alertClasses.Should().Contain("gap-0.5");
        alertClasses.Should().Contain("text-start");
        alertClasses.Should().Contain("has-data-[slot=alert-action]:relative");
        alertClasses.Should().Contain("has-data-[slot=alert-action]:pr-18");
        alertClasses.Should().Contain("has-[>svg]:grid-cols-[auto_1fr]");
        alertClasses.Should().Contain("has-[>svg]:gap-x-2");
        alertClasses.Should().Contain("*:[svg]:row-span-2");
        alertClasses.Should().Contain("*:[svg]:translate-y-0.5");
        alertClasses.Should().Contain("*:[svg]:text-current");
        alertClasses.Should().Contain("*:[svg:not([class*='size-'])]:size-4");
        alertClasses.Should().NotContain("q-alert");
    }
}

