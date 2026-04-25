using AwesomeAssertions;
using Bunit;
using System;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
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
        calendarClasses.Should().NotContain("p-3");
        calendarClasses.Should().NotContain("[--cell-size:--spacing(8)]");
        calendarClasses.Should().NotContain("rounded-lg");
        calendarClasses.Should().NotContain("border");
        calendarMode.Should().Be("single");

        var grid = cut.Find("[role='grid']");
        var monthDropdown = cut.Find("select.rdp-months_dropdown");
        var yearDropdown = cut.Find("select.rdp-years_dropdown");
        var dropdownRoot = cut.Find(".cn-calendar-dropdown-root");
        var captionLabel = cut.Find(".cn-calendar-caption-label");
        var dayButton = cut.Find(".rdp-day_button");
        var previousButton = cut.Find(".rdp-button_previous");
        var nextButton = cut.Find(".rdp-button_next");
        var monthOptions = monthDropdown.QuerySelectorAll("option");

        grid.GetAttribute("aria-label").Should().Contain(DateOnly.FromDateTime(DateTime.Today).ToString("MMMM"));
        monthDropdown.GetAttribute("class")!.Should().Contain("rdp-dropdown");
        monthDropdown.GetAttribute("aria-label")!.Should().Be("Choose the Month");
        yearDropdown.GetAttribute("class")!.Should().Contain("rdp-dropdown");
        yearDropdown.GetAttribute("aria-label")!.Should().Be("Choose the Year");
        dropdownRoot.GetAttribute("class")!.Should().Contain("rdp-dropdown_root");
        dropdownRoot.GetAttribute("class")!.Should().Contain("border-input");
        dropdownRoot.GetAttribute("class")!.Should().Contain("shadow-xs");
        dropdownRoot.GetAttribute("class")!.Should().Contain("has-focus:ring-[3px]");
        captionLabel.GetAttribute("class")!.Should().Contain("rdp-caption_label");
        captionLabel.GetAttribute("class")!.Should().Contain("h-8");
        captionLabel.GetAttribute("class")!.Should().Contain("pr-1");
        captionLabel.GetAttribute("class")!.Should().Contain("pl-2");
        captionLabel.GetAttribute("aria-hidden")!.Should().Be("true");
        dayButton.GetAttribute("aria-label").Should().NotBeNullOrWhiteSpace();
        dayButton.GetAttribute("class")!.Should().Contain("group/button");
        dayButton.GetAttribute("class")!.Should().Contain("rdp-day_button");
        dayButton.GetAttribute("class")!.Should().Contain("data-[selected-single=true]:bg-primary");
        dayButton.GetAttribute("class")!.Should().Contain("data-[range-middle=true]:bg-accent");
        previousButton.GetAttribute("aria-label")!.Should().Be("Go to the Previous Month");
        previousButton.GetAttribute("class")!.Should().Contain("size-(--cell-size)");
        nextButton.GetAttribute("aria-label")!.Should().Be("Go to the Next Month");
        nextButton.GetAttribute("class")!.Should().Contain("size-(--cell-size)");
        monthOptions[0].TextContent.Should().Be("Jan");
    }
}
