using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents an audio player component with support for HTML5 audio features.
/// </summary>
public interface IAudio : IElement
{
    /// <summary>
    /// Gets or sets the URL of the audio file.
    /// </summary>
    string? Source { get; set; }

    /// <summary>
    /// Gets or sets whether the audio should start playing automatically.
    /// </summary>
    bool Autoplay { get; set; }

    /// <summary>
    /// Gets or sets whether the audio should loop continuously.
    /// </summary>
    bool Loop { get; set; }

    /// <summary>
    /// Gets or sets whether the audio should be muted by default.
    /// </summary>
    bool Muted { get; set; }

    /// <summary>
    /// Gets or sets whether to display audio controls.
    /// </summary>
    bool Controls { get; set; }

    /// <summary>
    /// Gets or sets the preload strategy (auto, metadata, none).
    /// </summary>
    string? Preload { get; set; }

    /// <summary>
    /// Gets or sets the crossorigin attribute for CORS.
    /// </summary>
    string? CrossOrigin { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when audio metadata has loaded.
    /// </summary>
    EventCallback<ProgressEventArgs> OnLoadedMetadata { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the audio can start playing.
    /// </summary>
    EventCallback<EventArgs> OnCanPlay { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the audio starts playing.
    /// </summary>
    EventCallback<EventArgs> OnPlay { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the audio is paused.
    /// </summary>
    EventCallback<EventArgs> OnPause { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the audio has ended.
    /// </summary>
    EventCallback<EventArgs> OnEnded { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when an audio error occurs.
    /// </summary>
    EventCallback<ErrorEventArgs> OnError { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the playing position changes.
    /// </summary>
    EventCallback<EventArgs> OnTimeUpdate { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the volume changes.
    /// </summary>
    EventCallback<EventArgs> OnVolumeChange { get; set; }
}

