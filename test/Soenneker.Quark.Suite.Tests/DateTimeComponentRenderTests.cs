using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
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

    [Test]
    public void Date_relative_supports_short_format_style()
    {
        var cut = Render<DateRelative>(parameters => parameters
            .Add(component => component.Value, DateTimeOffset.UtcNow.AddHours(-5))
            .Add(component => component.FormatStyle, DateRelativeFormatStyle.Short)
            .Add(component => component.TimeZone, "UTC")
            .Add(component => component.AutoUpdate, false));

        var time = cut.Find("time[data-slot='date-relative']");

        time.TextContent.Should().Be("5 hr");
    }

    private sealed class StaticBrowserTimeZoneService : IQuarkBrowserTimeZoneService
    {
        public ValueTask<string?> GetTimeZoneId(CancellationToken cancellationToken = default) => ValueTask.FromResult<string?>("UTC");
    }

    [Test]
    public async Task Pending_time_zone_detection_does_not_register_after_disposal()
    {
        var detection = new PendingBrowserTimeZoneService();
        var scheduler = new CountingScheduler();
        using var services = new ServiceCollection().AddSingleton<IQuarkDateTimeScheduler>(scheduler).BuildServiceProvider();
        var probe = new DateTimeLifecycleProbe();
        probe.Configure(detection, services);
        var pendingRender = probe.AfterRender();
        pendingRender.IsCompleted.Should().BeFalse();
        await probe.DisposeAsync();
        detection.Completion.SetResult(null);
        await pendingRender;
        scheduler.Registrations.Should().Be(0);
    }

    [Test]
    public void Date_time_fragment_reuses_delegate_and_reads_updated_parameters()
    {
        var value = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var cut = Render<DateTimeLifecycleProbe>(p => p.Add(c => c.Value, value)
            .Add(c => c.Format, "yyyy").Add(c => c.Prefix, "Before ").Add(c => c.AutoUpdate, false));
        var fragment = cut.Instance.Content();
        cut.Render(p => p.Add(c => c.Value, value.AddYears(1)).Add(c => c.Prefix, "After "));
        cut.Instance.Content().Should().BeSameAs(fragment);
        cut.Find("time").TextContent.Should().Be("After 2027");
    }

    private sealed class PendingBrowserTimeZoneService : IQuarkBrowserTimeZoneService
    {
        public TaskCompletionSource<string?> Completion { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public ValueTask<string?> GetTimeZoneId(CancellationToken cancellationToken = default) => new(Completion.Task);
    }

    private sealed class CountingScheduler : IQuarkDateTimeScheduler, IQuarkDateTimeScheduleRegistration
    {
        public int Registrations { get; private set; }
        public IQuarkDateTimeScheduleRegistration Register(Func<DateTimeOffset, TimeSpan?> interval, Func<DateTimeOffset, ValueTask> callback)
        {
            Registrations++;
            return this;
        }
        public void Reschedule() { }
        public void Dispose() { }
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

public sealed class DateTimeLifecycleProbe : DateTimeText
{
    public void Configure(IQuarkBrowserTimeZoneService detection, IServiceProvider services)
    {
        BrowserTimeZoneService = detection;
        Services = services;
        RefreshInterval = TimeSpan.FromSeconds(1);
    }
    public Task AfterRender() => OnAfterRenderAsync(false);
    public RenderFragment Content() => RenderContent();
}
