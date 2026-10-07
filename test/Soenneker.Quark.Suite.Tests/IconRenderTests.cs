using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Lucide.Enums.Icons;
using Soenneker.Quark.Gen.Lucide.Abstractions;
using Soenneker.Quark.Gen.Lucide.Generated;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Icon_renders_generated_svg_without_provider_fallback()
    {
        Services.RemoveAll<ILucideIconSvgProvider>();
        Services.AddLucideIconsAsScoped();

        var cut = Render<Icon>(p => p.Add(c => c.Name, LucideIcon.ChevronDown));

        cut.Find("svg").GetAttribute("viewBox").Should().Be("0 0 24 24");
        cut.Find("svg path").GetAttribute("d").Should().Be("m6 9 6 6 6-6");
    }
}
