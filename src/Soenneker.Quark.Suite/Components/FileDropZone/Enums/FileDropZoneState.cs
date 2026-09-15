using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>The lifecycle of a file in a drop zone.</summary>
[EnumValue<string>]
public sealed partial class FileDropZoneState
{
    /// <summary>Waiting to upload.</summary>
    public static readonly FileDropZoneState Queued = new("queued");
    /// <summary>Transferring file bytes.</summary>
    public static readonly FileDropZoneState Uploading = new("uploading");
    /// <summary>The server is scanning or processing the upload.</summary>
    public static readonly FileDropZoneState Processing = new("processing");
    /// <summary>The file is available.</summary>
    public static readonly FileDropZoneState Complete = new("complete");
    /// <summary>An operation failed.</summary>
    public static readonly FileDropZoneState Failed = new("failed");
    /// <summary>The upload was canceled.</summary>
    public static readonly FileDropZoneState Canceled = new("canceled");
    /// <summary>Waiting for the server to delete the file.</summary>
    public static readonly FileDropZoneState Deleting = new("deleting");
}
