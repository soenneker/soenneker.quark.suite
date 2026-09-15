using System;

namespace Soenneker.Quark;

/// <summary>File metadata and presentation state. Saved files require no browser file or download.</summary>
public sealed record FileDropZoneFile
{
    /// <summary>A stable, unique UI key, independent of the server identifier.</summary>
    public string Id { get; init; } = Guid.NewGuid().ToString("N");
    /// <summary>The original display name.</summary>
    public required string Name { get; init; }
    /// <summary>The file size in bytes, or null when unknown.</summary>
    public long? Size { get; init; }
    /// <summary>The media MIME type. When omitted, preview rendering falls back to the filename extension.</summary>
    public string? ContentType { get; init; }
    /// <summary>A URL for an existing file's image, video, or PDF preview. The application owns access and URL lifetime.</summary>
    public string? PreviewUrl { get; init; }
    /// <summary>The identifier used by the application's delete handler.</summary>
    public string? ServerId { get; init; }
    /// <summary>The current lifecycle state. Metadata supplied for existing files defaults to complete.</summary>
    public FileDropZoneState State { get; init; } = FileDropZoneState.Complete;
    /// <summary>Progress of the current stage, from zero to 100; null means indeterminate.</summary>
    public int? Progress { get; init; }
    /// <summary>Optional stage text, such as “Checking for viruses”.</summary>
    public string? StatusText { get; init; }
    /// <summary>A user-facing operation error. Delete failures leave the file available for another attempt.</summary>
    public string? Error { get; init; }
}
