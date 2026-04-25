using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Typography_primitives_match_shadcn_base_classes()
    {
        var code = Render<Code>(parameters => parameters.Add(p => p.ChildContent, "npm i"));
        code.Find("[data-slot='code']").GetAttribute("class")!.Should().ContainAll("relative", "rounded", "bg-muted", "px-[0.3rem]", "py-[0.2rem]", "font-mono", "text-sm", "font-semibold");
    }

    [Test]
    public void Typography_headings_and_supporting_text_match_shadcn_examples()
    {
        var h1 = Render<H1>(parameters => parameters.Add(p => p.ChildContent, "Heading"));
        var h2 = Render<H2>(parameters => parameters.Add(p => p.ChildContent, "Heading"));
        var h3 = Render<H3>(parameters => parameters.Add(p => p.ChildContent, "Heading"));
        var h4 = Render<H4>(parameters => parameters.Add(p => p.ChildContent, "Heading"));
        var lead = Render<Lead>(parameters => parameters.Add(p => p.ChildContent, "Lead"));
        var muted = Render<Muted>(parameters => parameters.Add(p => p.ChildContent, "Muted"));
        var small = Render<Small>(parameters => parameters.Add(p => p.ChildContent, "Small"));

        h1.Find("[data-slot='h1']").GetAttribute("class")!.Should().ContainAll("scroll-m-20", "text-4xl", "font-extrabold", "tracking-tight", "text-balance");
        h1.Find("[data-slot='h1']").GetAttribute("class")!.Should().NotContain("lg:text-5xl");
        h2.Find("[data-slot='h2']").GetAttribute("class")!.Should().ContainAll("scroll-m-20", "border-b", "pb-2", "text-3xl", "font-semibold", "tracking-tight", "first:mt-0");
        h3.Find("[data-slot='h3']").GetAttribute("class")!.Should().ContainAll("scroll-m-20", "text-2xl", "font-semibold", "tracking-tight");
        h4.Find("[data-slot='h4']").GetAttribute("class")!.Should().ContainAll("scroll-m-20", "text-xl", "font-semibold", "tracking-tight");
        lead.Find("[data-slot='lead']").GetAttribute("class")!.Should().ContainAll("text-xl", "text-muted-foreground");
        muted.Find("[data-slot='muted']").GetAttribute("class")!.Should().ContainAll("text-sm", "text-muted-foreground");
        small.Find("[data-slot='small']").GetAttribute("class")!.Should().ContainAll("text-sm", "leading-none", "font-medium");
    }
}
