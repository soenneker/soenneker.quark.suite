using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Typography_primitives_match_shadcn_base_classes()
    {
        var code = Render<Code>(parameters => parameters.Add(p => p.ChildContent, "npm i"));
        code.Find("[data-slot='code']").GetAttribute("class")!.Should().ContainAll("relative", "rounded-md", "bg-muted", "px-[0.3rem]", "py-[0.2rem]", "font-mono", "text-sm", "font-semibold");
    }
}
