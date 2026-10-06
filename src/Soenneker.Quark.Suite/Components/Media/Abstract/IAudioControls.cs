using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Reusable audio controls for a player owned by the caller, including native audio players.</summary>
public interface IAudioControls : IElement
{
    /// <summary>Whether the player is currently playing.</summary>
    bool IsPlaying { get; set; }

    /// <summary>The duration in seconds. Zero indicates an unknown duration.</summary>
    double MediaDuration { get; set; }

    /// <summary>The current position in seconds.</summary>
    double CurrentTime { get; set; }

    /// <summary>The volume from zero to one.</summary>
    double Volume { get; set; }

    /// <summary>Disables all controls.</summary>
    bool Disabled { get; set; }

    /// <summary>Shows only the playback button.</summary>
    bool Compact { get; set; }

    /// <summary>Shows a stop action instead of pause while playing.</summary>
    bool StopInsteadOfPause { get; set; }

    /// <summary>Whether seeking is available.</summary>
    bool CanSeek { get; set; }

    /// <summary>Requests playback, pause, or stop. The caller updates IsPlaying after completing the action.</summary>
    EventCallback OnTogglePlayback { get; set; }

    /// <summary>Requests a playback position in seconds. The caller updates CurrentTime.</summary>
    EventCallback<double> CurrentTimeChanged { get; set; }

    /// <summary>Requests a volume from zero to one. The caller updates Volume.</summary>
    EventCallback<double> VolumeChanged { get; set; }
}
