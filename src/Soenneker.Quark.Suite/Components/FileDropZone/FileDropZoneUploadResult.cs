namespace Soenneker.Quark;

/// <summary>The persisted upload, either ready now or accepted for background processing.</summary>
public sealed record FileDropZoneUploadResult
{
    /// <summary>The nonempty persisted file identifier used for deletion.</summary>
    public required string ServerId { get; init; }
    /// <summary>Complete when ready, or Processing while a background server job continues.</summary>
    public FileDropZoneState State { get; init; } = FileDropZoneState.Complete;
    /// <summary>Optional background processing percentage; null means indeterminate.</summary>
    public int? Progress { get; init; }
    /// <summary>Optional background stage label, such as “Checking for viruses”.</summary>
    public string? StatusText { get; init; }
}
