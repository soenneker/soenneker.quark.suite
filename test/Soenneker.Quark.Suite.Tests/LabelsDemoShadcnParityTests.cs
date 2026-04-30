using AwesomeAssertions;
using Bunit;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Labels_demo_page_uses_shadcn_preview_overrides()
    {
        var cut = Render<global::Soenneker.Quark.Suite.Demo.Pages.Components.Labels>();

        cut.Find("[id='label-demo-terms']").Should().NotBeNull();
        cut.Find("label[for='label-demo-terms']").TextContent.Should().Be("Accept terms and conditions");
        cut.Find("[id='label-demo-username']").Should().NotBeNull();
        cut.Find("label[for='label-demo-username']").TextContent.Should().Be("Username");
        cut.Find("[id='label-demo-disabled']").HasAttribute("disabled").Should().BeTrue();
        cut.Find("label[for='label-demo-disabled']").TextContent.Should().Be("Disabled");
        cut.Find("[id='label-demo-message']").Should().NotBeNull();
        cut.Find("label[for='label-demo-message']").TextContent.Should().Be("Message");

        var rtlPreview = cut.FindAll("[data-slot='preview']").Single(node => node.GetAttribute("dir") == "rtl");
        rtlPreview.GetAttribute("data-lang").Should().Be("ar");
        rtlPreview.QuerySelector("[id='terms-rtl']").Should().NotBeNull();
        rtlPreview.QuerySelector("label[for='terms-rtl']")!.TextContent.Should().Be("قبول الشروط والأحكام");
    }
}
