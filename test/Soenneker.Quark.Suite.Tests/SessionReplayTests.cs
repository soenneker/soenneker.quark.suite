using System;
using System.Text.Json;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.Rrweb.Replay.Abstract;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed class SessionReplayTests : BunitContext
{
    private readonly SessionReplayTestInterop _interop = new();

    public SessionReplayTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddLogging();
        Services.AddQuarkSuiteAsScoped();
        Services.AddSingleton<IRrwebReplayInterop>(_interop);
    }

    private static JsonElement[] Recording() => JsonSerializer.Deserialize<JsonElement[]>(
        """[{"type":2,"timestamp":0,"data":{}},{"type":3,"timestamp":10000,"data":{}}]""")!;

    [Test]
    public void Empty_recording_does_not_create_a_player_and_disables_controls()
    {
        var cut = Render<SessionReplay>();
        cut.Find("[role='status']").TextContent.Should().Contain("No recording available");
        cut.Find("button").HasAttribute("disabled").Should().BeTrue();
        _interop.Calls.Should().BeEmpty();
    }

    [Test]
    public async Task Controls_seek_preserve_playback_clamp_offsets_and_restart_at_end(CancellationToken cancellationToken)
    {
        var cut = Render<SessionReplay>(p => p.Add(c => c.Events, Recording()));
        cut.WaitForAssertion(() => cut.Instance.RecordingDuration.Should().Be(10000));
        await cut.Find("button[aria-label='Play recording']").ClickAsync();
        cut.Instance.IsPlaying.Should().BeTrue();
        await cut.InvokeAsync(() => cut.Instance.Seek(5000));
        _interop.Position.Should().Be(5000);
        cut.Instance.IsPlaying.Should().BeTrue();
        await cut.Find("button[aria-label='Pause recording']").ClickAsync();
        await cut.InvokeAsync(() => cut.Instance.Seek(-100));
        _interop.Position.Should().Be(0);
        cut.Instance.IsPlaying.Should().BeFalse();
        await cut.Find("select").ChangeAsync(new Microsoft.AspNetCore.Components.ChangeEventArgs { Value = "2" });
        _interop.Speed.Should().Be(2);
        await cut.InvokeAsync(() => cut.Instance.Seek(20000));
        _interop.Position.Should().Be(10000);
        await cut.InvokeAsync(cut.Instance.Play);
        _interop.Position.Should().Be(0);
        cut.Instance.IsPlaying.Should().BeTrue();
        _interop.Position = 10000;
        cut.WaitForAssertion(() => cut.Instance.IsPlaying.Should().BeFalse());
    }

    [Test]
    public async Task Replacing_recording_destroys_old_player_and_empty_recording_resets_state(CancellationToken cancellationToken)
    {
        var events = Recording();
        var cut = Render<SessionReplay>(p => p.Add(c => c.Events, events));
        cut.Render(p => p.Add(c => c.Events, events));
        _interop.Calls.Should().Equal("create");
        cut.Render(p => p.Add(c => c.Events, Recording()));
        cut.WaitForAssertion(() => _interop.Calls.Should().Equal("create", "destroy", "create"));
        cut.Render(p => p.Add(c => c.Events, Array.Empty<JsonElement>()));
        cut.WaitForAssertion(() => cut.Instance.RecordingDuration.Should().Be(0));
        _interop.Calls.Should().Equal("create", "destroy", "create", "destroy");
        await cut.InvokeAsync(() => cut.Instance.DisposeAsync().AsTask());
        _interop.Calls.Should().HaveCount(4);
    }

    [Test]
    public void Failed_initialization_reports_error_and_can_load_a_replacement()
    {
        _interop.FailCreate = true;
        Exception? error = null;
        var cut = Render<SessionReplay>(p => p.Add(c => c.Events, Recording()).Add(c => c.OnError, ex => error = ex));
        cut.WaitForAssertion(() => error.Should().BeOfType<InvalidOperationException>());
        cut.Find("[role='alert']").TextContent.Should().Contain("Recording unavailable");
        _interop.FailCreate = false;
        cut.Render(p => p.Add(c => c.Events, Recording()));
        cut.WaitForAssertion(() => cut.FindAll("[role='alert']").Should().BeEmpty());
        cut.Instance.RecordingDuration.Should().Be(10000);
    }

    [Test]
    public async Task Disposal_during_creation_destroys_the_player_after_creation_completes(CancellationToken cancellationToken)
    {
        _interop.PendingCreate = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var cut = Render<SessionReplay>(p => p.Add(c => c.Events, Recording()));
        cut.WaitForAssertion(() => _interop.Calls.Should().Contain("create"));
        Task disposing = cut.InvokeAsync(() => cut.Instance.DisposeAsync().AsTask());
        _interop.PendingCreate.SetResult();
        await disposing;
        _interop.Calls.Should().Equal("create", "destroy");
    }
}
