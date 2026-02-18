using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents a video player component with support for HTML5 video features.
/// </summary>
public interface IVideo : IElement
{
    /// <summary>
    /// Gets or sets the URL of the video file.
    /// </summary>
    string? Source { get; set; }

    /// <summary>
    /// Gets or sets the URL of an image to show before the video plays.
    /// </summary>
    string? Poster { get; set; }

    /// <summary>
    /// Gets or sets whether the video should start playing automatically.
    /// </summary>
    bool Autoplay { get; set; }

    /// <summary>
    /// Gets or sets whether the video should loop continuously.
    /// </summary>
    bool Loop { get; set; }

    /// <summary>
    /// Gets or sets whether the video should be muted by default.
    /// </summary>
    bool Muted { get; set; }

    /// <summary>
    /// Gets or sets whether to display video controls.
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
    /// Gets or sets whether the video should play inline on mobile devices.
    /// </summary>
    bool PlaysInline { get; set; }

    /// <summary>
    /// Gets or sets whether to disable remote playback.
    /// </summary>
    bool DisableRemotePlayback { get; set; }

    /// <summary>
    /// Gets or sets whether to disable picture-in-picture mode.
    /// </summary>
    bool DisablePictureInPicture { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When video metadata has loaded.
    /// </summary>
    EventCallback<ProgressEventArgs> OnLoadedMetadata { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the video can start playing.
    /// </summary>
    EventCallback<EventArgs> OnCanPlay { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the video starts playing.
    /// </summary>
    EventCallback<EventArgs> OnPlay { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the video is paused.
    /// </summary>
    EventCallback<EventArgs> OnPause { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the video has ended.
    /// </summary>
    EventCallback<EventArgs> OnEnded { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When a video error occurs.
    /// </summary>
    EventCallback<ErrorEventArgs> OnError { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the playing position changes.
    /// </summary>
    EventCallback<EventArgs> OnTimeUpdate { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the volume changes.
    /// </summary>
    EventCallback<EventArgs> OnVolumeChange { get; set; }
}

