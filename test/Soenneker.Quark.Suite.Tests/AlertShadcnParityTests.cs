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

        var alertClasses = alert.Find("[data-slot='alert']").GetAttribute("class")!;

        alertClasses.Should().Contain("group/alert");
        alertClasses.Should().Contain("relative");
        alertClasses.Should().Contain("grid");
        alertClasses.Should().Contain("w-full");
        alertClasses.Should().Contain("grid-cols-[0_1fr]");
        alertClasses.Should().Contain("gap-y-0.5");
        alertClasses.Should().Contain("has-data-[slot=alert-action]:relative");
        alertClasses.Should().Contain("has-data-[slot=alert-action]:pr-18");
        alertClasses.Should().Contain("has-[>svg]:grid-cols-[calc(var(--spacing)*4)_1fr]");
        alertClasses.Should().Contain("has-[>svg]:gap-x-3");
        alertClasses.Should().Contain("[&>svg]:size-4");
        alertClasses.Should().Contain("[&>svg]:translate-y-0.5");
        alertClasses.Should().Contain("[&>svg]:text-current");
        alertClasses.Should().NotContain("gap-0.5");
        alertClasses.Should().NotContain("q-alert");
    }
}

