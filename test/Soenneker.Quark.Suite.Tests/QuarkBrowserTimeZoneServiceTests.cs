using System;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Extensions.Logging.Abstractions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class QuarkBrowserTimeZoneServiceTests
{
    [Test]
    public async Task Browser_timezone_service_returns_null_when_js_fails()
    {
        var interop = new ThrowingBrowserTimeZoneInterop();
        var service = new QuarkBrowserTimeZoneService(interop, NullLogger<QuarkBrowserTimeZoneService>.Instance);

        var result = await service.GetTimeZoneId();

        result.Should().BeNull();
    }

    [Test]
    public async Task Browser_timezone_service_caches_detected_timezone()
    {
        var interop = new CountingBrowserTimeZoneInterop("UTC");
        var service = new QuarkBrowserTimeZoneService(interop, NullLogger<QuarkBrowserTimeZoneService>.Instance);

        var first = await service.GetTimeZoneId();
        var second = await service.GetTimeZoneId();

        first.Should().Be("UTC");
        second.Should().Be("UTC");
        interop.Count.Should().Be(1);
    }

    private sealed class ThrowingBrowserTimeZoneInterop : IQuarkBrowserTimeZoneInterop
    {
        public ValueTask<string?> GetTimeZoneId(CancellationToken cancellationToken = default) => throw new InvalidOperationException("JS failed");

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class CountingBrowserTimeZoneInterop : IQuarkBrowserTimeZoneInterop
    {
        private readonly string? _timeZone;

        public int Count { get; private set; }

        public CountingBrowserTimeZoneInterop(string? timeZone)
        {
            _timeZone = timeZone;
        }

        public ValueTask<string?> GetTimeZoneId(CancellationToken cancellationToken = default)
        {
            Count++;
            return ValueTask.FromResult(_timeZone);
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
