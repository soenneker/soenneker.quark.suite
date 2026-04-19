using AwesomeAssertions;
using Bunit;
using System.Linq;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Labels_demo_page_uses_shadcn_preview_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.Labels>();

        cut.FindAll("[data-slot='preview']")
            .Select(node => node.FirstElementChild!.GetAttribute("class")!)
            .Should().Contain(cls => cls.Contains("h-[44rem]"));

        cut.Find("[id='terms']").Should().NotBeNull();
        cut.Find("label[for='terms']").TextContent.Should().Be("Accept terms and conditions");

        var rtlPreview = cut.FindAll("[data-slot='preview']").Single(node => node.GetAttribute("dir") == "rtl");
        rtlPreview.GetAttribute("data-lang").Should().Be("ar");
        rtlPreview.QuerySelector("[id='terms-rtl']").Should().NotBeNull();
        rtlPreview.QuerySelector("label[for='terms-rtl']")!.TextContent.Should().Be("قبول الشروط والأحكام");
    }
}
