using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Quark.Suite.Demo.Pages;
using System.Linq;
using Soenneker.Bradix;
using Soenneker.Blazor.MockJsRuntime.Registrars;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Category_navigation_refreshes_the_existing_gallery()
    {
        var navigation = Services.GetRequiredService<NavigationManager>();
        navigation.NavigateTo("/charts/area");
        var cut = Render<ChartGallery>();

        foreach (var category in new[] { "Bar", "Line", "Pie", "Radar", "Radial", "Area" })
        {
            var tab = cut.FindAll("[role='tab']").Single(element => element.TextContent == $"{category} Charts");
            tab.MouseDown();

            cut.WaitForAssertion(() =>
            {
                cut.Find("#chart-heading").TextContent.Should().Be($"{category} Chart");
                cut.Find("[role='tab'][aria-selected='true']").TextContent.Should().Be($"{category} Charts");
                navigation.Uri.Should().EndWith($"/charts/{category.ToLowerInvariant()}");
                cut.Find("[role='tabpanel']").Id.Should().Be(cut.Find("[aria-selected='true']").GetAttribute("aria-controls"));
                cut.Find(".chart-gallery-grid h3").TextContent.Should().Be($"{category} Chart - Default");
            });
        }

        cut.FindAll("[role='tab']").Single(element => element.TextContent == "Pie Charts").KeyDown("Enter");
        cut.WaitForAssertion(() => cut.Find("#chart-heading").TextContent.Should().Be("Pie Chart"));

        navigation.NavigateTo("/charts/bar");
        cut.WaitForAssertion(() => cut.Find("[role='tab'][aria-selected='true']").TextContent.Should().Be("Bar Charts"));

        navigation.NavigateTo("/charts");
        cut.WaitForAssertion(() => cut.Find("#chart-heading").TextContent.Should().Be("Area Chart"));

        cut.Find("select[aria-label='Chart date range']").Change("7");
        cut.WaitForAssertion(() => cut.Find(".chart-gallery-featured p").TextContent.Should().Contain("7 days"));
        cut.FindAll("button").Single(element => element.TextContent == "View Code").Click();
        cut.Find("pre code").TextContent.Should().Contain("ChartType.Area");
        cut.FindAll("button").Single(element => element.TextContent == "Hide Code").Click();
        cut.FindAll("pre").Should().BeEmpty();
    }
}
