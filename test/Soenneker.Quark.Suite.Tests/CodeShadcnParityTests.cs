using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Code_matches_shadcn_base_classes()
    {
        var code = Render<Code>(parameters => parameters.Add(p => p.ChildContent, "npm i"));
        var classes = code.Find("[data-slot='code']").GetAttribute("class")!;

        classes.Should().ContainAll("relative", "rounded", "bg-muted", "px-[0.3rem]", "py-[0.2rem]", "font-mono", "text-sm", "font-semibold");
        classes.Should().NotContain("rounded-md");
    }
}
