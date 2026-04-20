using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Calendar_matches_shadcn_default_component_contract()
    {
        var cut = Render<Calendar>(parameters => parameters
            .Add(p => p.CaptionLayout, "dropdown")
            .Add(p => p.CultureName, "en-US"));

        var calendar = cut.Find("[data-slot='calendar']");
        var calendarClasses = calendar.GetAttribute("class")!;
        var calendarMode = calendar.GetAttribute("data-mode")!;

        calendarClasses.Should().Contain("w-fit");
        calendarClasses.Should().Contain("rdp-root");
        calendarClasses.Should().Contain("group/calendar");
        calendarClasses.Should().Contain("bg-background");
        calendarClasses.Should().Contain("p-2");
        calendarClasses.Should().Contain("[--cell-radius:var(--radius-md)]");
        calendarClasses.Should().Contain("[--cell-size:--spacing(7)]");
        calendarClasses.Should().Contain("rounded-lg");
        calendarClasses.Should().Contain("border");
        calendarMode.Should().Be("single");

        var monthDropdown = cut.Find("select.rdp-months_dropdown");
        var yearDropdown = cut.Find("select.rdp-years_dropdown");
        var dropdownRoot = cut.Find(".cn-calendar-dropdown-root");
        var captionLabel = cut.Find(".cn-calendar-caption-label");
        var dayButton = cut.Find(".rdp-day_button");
        var previousButton = cut.Find(".rdp-button_previous");
        var nextButton = cut.Find(".rdp-button_next");
        var monthOptions = monthDropdown.QuerySelectorAll("option");

        monthDropdown.GetAttribute("class")!.Should().Contain("rdp-dropdown");
        monthDropdown.GetAttribute("aria-label")!.Should().Be("Choose the Month");
        yearDropdown.GetAttribute("class")!.Should().Contain("rdp-dropdown");
        yearDropdown.GetAttribute("aria-label")!.Should().Be("Choose the Year");
        dropdownRoot.GetAttribute("class")!.Should().Contain("rdp-dropdown_root");
        captionLabel.GetAttribute("class")!.Should().Contain("rdp-caption_label");
        captionLabel.GetAttribute("aria-hidden")!.Should().Be("true");
        dayButton.GetAttribute("class")!.Should().Contain("group/button");
        dayButton.GetAttribute("class")!.Should().Contain("rdp-day_button");
        dayButton.GetAttribute("class")!.Should().Contain("data-[selected-single=true]:bg-primary");
        previousButton.GetAttribute("aria-label")!.Should().Be("Go to the Previous Month");
        previousButton.GetAttribute("class")!.Should().Contain("size-(--cell-size)");
        nextButton.GetAttribute("aria-label")!.Should().Be("Go to the Next Month");
        nextButton.GetAttribute("class")!.Should().Contain("size-(--cell-size)");
        monthOptions[0].TextContent.Should().Be("Jan");
    }
}
