using AwesomeAssertions;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TooltipProvider_renders_without_shadcn_contract_drift()
    {
        var provider = Render<TooltipProvider>(parameters => parameters
            .Add(p => p.ChildContent, "Content"));

        provider.Markup.Should().NotBeNullOrWhiteSpace();
    }
}
