using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Tests;

public sealed class CitationCacheTests : BunitContext
{
    public CitationCacheTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddBradixSuiteAsScoped();
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public void Resolution_reuses_unchanged_sources_and_observes_array_replacements()
    {
        CitationSourceInput[] sources = [new() { Url = "https://www.example.com/first" }];
        var cut = Render<Citation>(p => p.Add(x => x.Citations, sources));
        var first = cut.Instance.ResolvedCitations[0];
        cut.Render();
        cut.Instance.ResolvedCitations[0].Should().BeSameAs(first);
        sources[0] = new() { Url = "https://www.example.com/second" };
        cut.Render(p => p.Add(x => x.Citations, sources));
        cut.Instance.ResolvedCitations[0].Should().NotBeSameAs(first);
        cut.Render(p => p.Add(x => x.Citations, []));
        cut.Instance.ResolvedCitations.Should().BeEmpty();
    }
}
