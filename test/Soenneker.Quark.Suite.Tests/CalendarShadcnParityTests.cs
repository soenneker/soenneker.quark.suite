using AwesomeAssertions;
using Bunit;
using System;
using System.IO;


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
        calendarClasses.Should().Contain("p-3");
        calendarClasses.Should().Contain("[--cell-size:--spacing(8)]");
        calendarClasses.Should().Contain("[[data-slot=card-content]_&]:bg-transparent");
        calendarClasses.Should().Contain("[[data-slot=popover-content]_&]:bg-transparent");
        calendarClasses.Should().NotContain("p-2");
        calendarClasses.Should().NotContain("[--cell-radius:var(--radius-md)]");
        calendarClasses.Should().NotContain("[--cell-size:--spacing(7)]");
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
        dropdownRoot.GetAttribute("class")!.Should().Contain("rounded-md");
        dropdownRoot.GetAttribute("class")!.Should().Contain("border");
        dropdownRoot.GetAttribute("class")!.Should().Contain("border-input");
        dropdownRoot.GetAttribute("class")!.Should().Contain("shadow-xs");
        dropdownRoot.GetAttribute("class")!.Should().Contain("has-focus:ring-[3px]");
        captionLabel.GetAttribute("class")!.Should().Contain("rdp-caption_label");
        captionLabel.GetAttribute("class")!.Should().Contain("h-8");
        captionLabel.GetAttribute("class")!.Should().Contain("rounded-md");
        captionLabel.GetAttribute("class")!.Should().Contain("pr-1");
        captionLabel.GetAttribute("class")!.Should().Contain("pl-2");
        captionLabel.GetAttribute("aria-hidden")!.Should().Be("true");
        dayButton.GetAttribute("aria-label").Should().NotBeNullOrWhiteSpace();
        dayButton.GetAttribute("data-day").Should().Contain("/");
        dayButton.GetAttribute("class")!.Should().Contain("rdp-day_button");
        dayButton.GetAttribute("class")!.Should().Contain("rounded-md");
        dayButton.GetAttribute("class")!.Should().Contain("min-w-(--cell-size)");
        dayButton.GetAttribute("class")!.Should().Contain("data-[selected-single=true]:bg-primary");
        dayButton.GetAttribute("class")!.Should().Contain("data-[range-middle=true]:bg-accent");
        dayButton.GetAttribute("class")!.Should().NotContain("group/button");
        dayButton.GetAttribute("class")!.Should().NotContain("rounded-lg");
        dayButton.GetAttribute("class")!.Should().NotContain("min-w-7");
        previousButton.GetAttribute("aria-label")!.Should().Be("Go to the Previous Month");
        previousButton.GetAttribute("class")!.Should().Contain("size-(--cell-size)");
        nextButton.GetAttribute("aria-label")!.Should().Be("Go to the Next Month");
        nextButton.GetAttribute("class")!.Should().Contain("size-(--cell-size)");
        monthOptions[0].TextContent.Should().Be("Jan");
    }

    [Test]
    public void Calendar_range_cells_match_shadcn_endpoint_classes()
    {
        var cut = Render<Calendar>(parameters => parameters
            .Add(p => p.Mode, CalendarSelectionMode.Range)
            .Add(p => p.DisplayMonth, new DateOnly(2026, 3, 1))
            .Add(p => p.SelectedRange, new CalendarDateRange(new DateOnly(2026, 3, 10), new DateOnly(2026, 3, 15)))
            .Add(p => p.CultureName, "en-US"));

        var rangeStart = cut.Find("button[data-range-start='true']");
        var rangeEnd = cut.Find("button[data-range-end='true']");
        var rangeMiddle = cut.Find("button[data-range-middle='true']");
        var startCellClasses = rangeStart.ParentElement!.GetAttribute("class")!;
        var endCellClasses = rangeEnd.ParentElement!.GetAttribute("class")!;

        startCellClasses.Should().Contain("rounded-l-md");
        startCellClasses.Should().Contain("bg-accent");
        startCellClasses.Should().NotContain("data-[selected=true]:rounded-none");
        endCellClasses.Should().Contain("rounded-r-md");
        endCellClasses.Should().Contain("bg-accent");
        endCellClasses.Should().NotContain("data-[selected=true]:rounded-none");
        rangeStart.GetAttribute("class")!.Should().Contain("data-[range-start=true]:bg-primary");
        rangeEnd.GetAttribute("class")!.Should().Contain("data-[range-end=true]:bg-primary");
        rangeMiddle.GetAttribute("class")!.Should().Contain("data-[range-middle=true]:bg-accent");
    }

    [Test]
    public void Calendar_range_navigation_keeps_user_navigated_month_when_display_month_parameter_is_unchanged()
    {
        CalendarDateRange? selectedRange = new(new DateOnly(2026, 3, 10), null);

        var cut = Render<Calendar>(parameters => parameters
            .Add(p => p.Mode, CalendarSelectionMode.Range)
            .Add(p => p.DisplayMonth, new DateOnly(2026, 3, 1))
            .Add(p => p.SelectedRange, selectedRange)
            .Add(p => p.SelectedRangeChanged, value => selectedRange = value)
            .Add(p => p.CultureName, "en-US"));

        cut.Find(".rdp-button_next").Click();
        cut.Find("[role='grid']").GetAttribute("aria-label").Should().Be("April 2026");

        cut.Find("button[aria-label='Wednesday, April 15, 2026']").Click();

        selectedRange.Should().NotBeNull();
        selectedRange!.From.Should().Be(new DateOnly(2026, 3, 10));
        selectedRange.To.Should().Be(new DateOnly(2026, 4, 15));
    }

    [Test]
    public void Calendar_range_demo_does_not_cap_next_month_selection()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForCalendar(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "Calendars.razor"));
        var rangeStart = source.IndexOf("Title=\"Range Calendar\"", StringComparison.Ordinal);
        var dropdownStart = source.IndexOf("Title=\"Month and Year Selector\"", StringComparison.Ordinal);
        var rangeDemo = source[rangeStart..dropdownStart];

        rangeDemo.Should().Contain("Mode=\"CalendarSelectionMode.Range\"");
        rangeDemo.Should().Contain("NumberOfMonths=\"2\"");
        rangeDemo.Should().NotContain("Min=\"_rangeMin\"");
        rangeDemo.Should().NotContain("Max=\"_rangeMax\"");
    }

    private static string GetSuiteRootForCalendar()
    {
        var directory = AppContext.BaseDirectory;

        while (!File.Exists(Path.Combine(directory, "Soenneker.Quark.Suite.slnx")))
            directory = Directory.GetParent(directory)!.FullName;

        return directory;
    }
}
