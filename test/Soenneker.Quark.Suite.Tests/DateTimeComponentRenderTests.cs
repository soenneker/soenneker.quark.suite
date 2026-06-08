using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class DateTimeComponentRenderTests : BunitContext
{
    public DateTimeComponentRenderTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddScoped<IQuarkDateTimeFormatter, QuarkDateTimeFormatter>();
        Services.AddScoped<IQuarkBrowserTimeZoneService, StaticBrowserTimeZoneService>();
    }

    [Test]
    public void Date_time_text_renders_semantic_time_with_merged_classes()
    {
        var cut = Render<DateTimeText>(parameters => parameters
            .Add(component => component.Value, new DateTimeOffset(2026, 1, 1, 15, 30, 0, TimeSpan.Zero))
            .Add(component => component.Format, "MMM d, yyyy h:mm tt")
            .Add(component => component.TimeZone, "UTC")
            .Add(component => component.Culture, CultureInfo.GetCultureInfo("en-US"))
            .Add(component => component.Class, "text-primary")
            .Add(component => component.Attributes, new Dictionary<string, object>
            {
                ["data-testid"] = "absolute-date"
            }));

        var time = cut.Find("time[data-slot='date-time-text']");

        time.TextContent.Should().Be("Jan 1, 2026 3:30 PM");
        time.GetAttribute("datetime").Should().StartWith("2026-01-01T15:30:00");
        time.GetAttribute("title").Should().Contain("Thursday, January 1, 2026");
        time.GetAttribute("class").Should().Contain("inline-flex");
        time.GetAttribute("class").Should().Contain("text-primary");
        time.GetAttribute("data-testid").Should().Be("absolute-date");
    }

    [Test]
    public void Date_relative_supports_null_text()
    {
        var cut = Render<DateRelative>(parameters => parameters
            .Add(component => component.Value, null)
            .Add(component => component.NullText, "No date"));

        var time = cut.Find("time[data-slot='date-relative']");

        time.TextContent.Should().Be("No date");
        time.HasAttribute("datetime").Should().BeFalse();
    }

    private sealed class StaticBrowserTimeZoneService : IQuarkBrowserTimeZoneService
    {
        public ValueTask<string?> GetTimeZoneId(CancellationToken cancellationToken = default) => ValueTask.FromResult<string?>("UTC");
    }
}
