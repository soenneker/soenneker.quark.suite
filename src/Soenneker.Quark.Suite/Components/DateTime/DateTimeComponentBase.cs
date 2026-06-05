using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Soenneker.Quark;

/// <summary>
/// Shared base for Quark date/time components.
/// </summary>
public abstract class DateTimeComponentBase : Element
{
    private CancellationTokenSource? _timerCts;
    private Task? _timerTask;
    private bool _browserTimeZoneResolved;
    private bool _restartTimerAfterRender = true;
    private string? _browserTimeZone;
    private string _displayText = string.Empty;
    private string? _displayTitle;
    private string? _dateTimeAttribute;

    /// <summary>
    /// Gets or sets an optional .NET date/time format string.
    /// </summary>
    [Parameter]
    public string? Format { get; set; }

    /// <summary>
    /// Gets or sets the time zone identifier used for display.
    /// </summary>
    [Parameter]
    public string? TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the culture used for localized date/time formatting.
    /// </summary>
    [Parameter]
    public CultureInfo? Culture { get; set; }

    /// <summary>
    /// Gets or sets a culture name used when <see cref="Culture"/> is not supplied.
    /// </summary>
    [Parameter]
    public string? CultureName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the component updates automatically.
    /// </summary>
    [Parameter]
    public bool AutoUpdate { get; set; } = true;

    /// <summary>
    /// Gets or sets an explicit refresh interval. When omitted, a smart interval is used.
    /// </summary>
    [Parameter]
    public TimeSpan? RefreshInterval { get; set; }

    /// <summary>
    /// Gets or sets text rendered before the formatted date/time text.
    /// </summary>
    [Parameter]
    public string? Prefix { get; set; }

    /// <summary>
    /// Gets or sets text rendered after the formatted date/time text.
    /// </summary>
    [Parameter]
    public string? Suffix { get; set; }

    /// <summary>
    /// Gets or sets text rendered when the component value is null.
    /// </summary>
    [Parameter]
    public string? NullText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a template for rendering the formatted text.
    /// </summary>
    [Parameter]
    public RenderFragment<string>? Template { get; set; }

    /// <summary>
    /// Gets the formatted display text.
    /// </summary>
    protected string DisplayText => _displayText;

    /// <summary>
    /// Gets the formatter used by the component.
    /// </summary>
    [Inject]
    protected IQuarkDateTimeFormatter Formatter { get; set; } = null!;

    /// <summary>
    /// Gets the browser time zone service used by the component.
    /// </summary>
    [Inject]
    protected IQuarkBrowserTimeZoneService BrowserTimeZoneService { get; set; } = null!;

    /// <summary>
    /// Gets the formatter kind for the component.
    /// </summary>
    protected abstract QuarkDateTimeUpdateKind UpdateKind { get; }

    /// <summary>
    /// Gets the value currently rendered by the component.
    /// </summary>
    /// <param name="now">The current instant.</param>
    /// <returns>The value to render, or <c>null</c>.</returns>
    protected abstract DateTimeOffset? GetEffectiveValue(DateTimeOffset now);

    /// <summary>
    /// Formats the component-specific text.
    /// </summary>
    /// <param name="value">The value being rendered.</param>
    /// <param name="now">The current instant.</param>
    /// <param name="options">Formatting options.</param>
    /// <returns>The formatted text.</returns>
    protected abstract string FormatValue(DateTimeOffset? value, DateTimeOffset now,
        QuarkDateTimeFormatOptions options);

    /// <summary>
    /// Gets the component-specific expired text.
    /// </summary>
    protected virtual string? ExpiredTextCore => null;

    /// <summary>
    /// Gets a value indicating whether the component should emit a datetime attribute.
    /// </summary>
    protected virtual bool ShouldEmitDateTimeAttribute => true;


    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        UpdateDisplay(DateTimeOffset.UtcNow);
        _restartTimerAfterRender = true;
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!_browserTimeZoneResolved)
        {
            _browserTimeZoneResolved = true;
            var timeZone = await BrowserTimeZoneService.GetTimeZoneId();

            if (!string.Equals(_browserTimeZone, timeZone, StringComparison.Ordinal))
            {
                _browserTimeZone = timeZone;

                if (UpdateDisplay(DateTimeOffset.UtcNow))
                    await InvokeAsync(StateHasChanged);
            }
        }

        if (_restartTimerAfterRender)
        {
            _restartTimerAfterRender = false;
            await RestartTimer();
        }
    }

    protected override void BuildAttributesCore(Dictionary<string, object> attributes)
    {
        base.BuildAttributesCore(attributes);

        if (ShouldEmitDateTimeAttribute && !string.IsNullOrWhiteSpace(_dateTimeAttribute))
            attributes["datetime"] = _dateTimeAttribute!;

        if (!attributes.ContainsKey("title") && !string.IsNullOrWhiteSpace(_displayTitle))
            attributes["title"] = _displayTitle!;

        BuildClassAttribute(attributes, (ref cls) =>
        {
            AppendClass(ref cls, "inline-flex items-center gap-1 tabular-nums");
        });
    }

    /// <summary>
    /// Renders the formatted text, template, or child content.
    /// </summary>
    /// <returns>A render fragment.</returns>
    protected RenderFragment RenderContent()
    {
        return builder =>
        {
            if (Template is not null)
            {
                builder.AddContent(0, Template(_displayText));
                return;
            }

            if (ChildContent is not null)
            {
                builder.AddContent(1, ChildContent);
                return;
            }

            if (!string.IsNullOrEmpty(Prefix))
                builder.AddContent(2, Prefix);

            builder.AddContent(3, _displayText);

            if (!string.IsNullOrEmpty(Suffix))
                builder.AddContent(4, Suffix);
        };
    }

    private bool UpdateDisplay(DateTimeOffset now)
    {
        var value = GetEffectiveValue(now);
        var options = BuildOptions();
        var text = FormatValue(value, now, options);
        var title = value.HasValue ? Formatter.FormatTitle(value.Value, options) : null;
        var dateTime = value.HasValue ? Formatter.FormatDateTimeAttribute(value.Value, options) : null;

        if (string.Equals(_displayText, text, StringComparison.Ordinal) &&
            string.Equals(_displayTitle, title, StringComparison.Ordinal) &&
            string.Equals(_dateTimeAttribute, dateTime, StringComparison.Ordinal))
        {
            return false;
        }

        _displayText = text;
        _displayTitle = title;
        _dateTimeAttribute = dateTime;

        return true;
    }

    private QuarkDateTimeFormatOptions BuildOptions()
    {
        return new QuarkDateTimeFormatOptions
        {
            Format = Format,
            TimeZone = TimeZone,
            BrowserTimeZone = _browserTimeZone,
            Culture = Culture,
            CultureName = CultureName,
            NullText = NullText,
            ExpiredText = ExpiredTextCore
        };
    }

    private async Task RestartTimer()
    {
        await StopTimer();

        if (!AutoUpdate)
            return;

        var interval = GetNextInterval(DateTimeOffset.UtcNow);

        if (!interval.HasValue)
            return;

        _timerCts = new CancellationTokenSource();
        _timerTask = RunTimer(_timerCts.Token);
    }

    private TimeSpan? GetNextInterval(DateTimeOffset now)
    {
        if (RefreshInterval.HasValue)
            return NormalizeInterval(RefreshInterval.Value);

        var value = GetEffectiveValue(now);

        if (!value.HasValue)
            return null;

        return NormalizeInterval(Formatter.GetNextUpdateInterval(UpdateKind, value.Value, now, BuildOptions()));
    }

    private static TimeSpan? NormalizeInterval(TimeSpan? interval)
    {
        if (!interval.HasValue)
            return null;

        if (interval.Value <= TimeSpan.Zero)
            return null;

        return interval.Value < TimeSpan.FromMilliseconds(250) ? TimeSpan.FromMilliseconds(250) : interval.Value;
    }

    private async Task RunTimer(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var now = DateTimeOffset.UtcNow;
                var interval = GetNextInterval(now);

                if (!interval.HasValue)
                    return;

                using var timer = new PeriodicTimer(interval.Value);

                if (!await timer.WaitForNextTickAsync(cancellationToken))
                    return;

                if (UpdateDisplay(DateTimeOffset.UtcNow))
                    await InvokeAsync(StateHasChanged);
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Quark date/time auto-update failed.");
        }
    }

    private async Task StopTimer()
    {
        var cts = _timerCts;
        var task = _timerTask;

        _timerCts = null;
        _timerTask = null;

        if (cts is not null)
        {
            await cts.CancelAsync();
            cts.Dispose();
        }

        if (task is not null)
        {
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
            }
        }
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async ValueTask DisposeAsync()
    {
        await StopTimer();
        await base.DisposeAsync();
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        hc.Add(Format);
        hc.Add(TimeZone);
        hc.Add(Culture);
        hc.Add(CultureName);
        hc.Add(AutoUpdate);
        hc.Add(RefreshInterval);
        hc.Add(Prefix);
        hc.Add(Suffix);
        hc.Add(NullText);
        hc.Add(ExpiredTextCore);
        hc.Add(_browserTimeZone);
        hc.Add(_displayText);
        hc.Add(_displayTitle);
        hc.Add(_dateTimeAttribute);
        hc.Add(Template is not null);
    }
}