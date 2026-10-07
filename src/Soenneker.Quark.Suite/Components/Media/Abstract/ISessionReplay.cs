using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Rrweb.Replay.Configuration;

namespace Soenneker.Quark;

/// <summary>A themed player for a complete rrweb session recording.</summary>
public interface ISessionReplay : IElement
{
    /// <summary>The complete recording, including its initial snapshot. Replace the array to load another session.</summary>
    JsonElement[] Events { get; set; }
    /// <summary>Replay configuration. Replace the instance to rebuild the player. Live mode is not supported.</summary>
    RrwebReplayOptions Options { get; set; }
    /// <summary>Whether to load pinned CDN assets instead of bundled assets. The first player in a scope selects the asset source.</summary>
    bool UseCdn { get; set; }
    /// <summary>Starts playback when a recording is loaded.</summary>
    bool AutoPlay { get; set; }
    /// <summary>Displays playback controls. Defaults to true.</summary>
    bool ShowControls { get; set; }
    /// <summary>Content shown when fewer than two events are supplied.</summary>
    RenderFragment? EmptyContent { get; set; }
    /// <summary>Invoked when the recording is ready for playback.</summary>
    EventCallback OnReady { get; set; }
    /// <summary>Invoked when initialization or a playback operation fails.</summary>
    EventCallback<Exception> OnError { get; set; }
    /// <summary>Whether playback is active.</summary>
    bool IsPlaying { get; }
    /// <summary>The current playback offset in milliseconds.</summary>
    double CurrentTime { get; }
    /// <summary>The recording duration in milliseconds.</summary>
    double RecordingDuration { get; }
    /// <summary>Resumes playback, restarting at zero when the recording has ended. Has no effect before the player is ready.</summary>
    Task Play();
    /// <summary>Pauses playback. Has no effect before the player is ready.</summary>
    Task Pause();
    /// <summary>Seeks to a millisecond offset, clamped to the recording bounds, preserving the playback state.</summary>
    Task Seek(double timeOffset);
    /// <summary>Sets a finite, positive playback speed multiplier.</summary>
    Task SetSpeed(double speed);
}
