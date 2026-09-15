using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>A themed file picker and drop target with application-owned upload and deletion transports.</summary>
public interface IFileDropZone : IElement
{
    /// <summary>Files to display, including saved metadata. Use two-way binding to retain lifecycle updates. Replace records to apply external server updates.</summary>
    IReadOnlyList<FileDropZoneFile> Files { get; set; }
    /// <summary>Called when selection, progress, completion, or deletion changes the file collection.</summary>
    EventCallback<IReadOnlyList<FileDropZoneFile>> FilesChanged { get; set; }
    /// <summary>Uploads a file and returns its persisted server identifier with Complete or Processing state. Honor cancellation; clean up partial uploads server-side. Exceptions become row errors.</summary>
    Func<FileDropZoneUploadRequest, CancellationToken, ValueTask<FileDropZoneUploadResult>>? Upload { get; set; }
    /// <summary>Deletes a persisted file. The row is removed only after success. Without this callback persisted files cannot be deleted.</summary>
    Func<FileDropZoneFile, CancellationToken, ValueTask>? Delete { get; set; }
    /// <summary>Comma-separated extensions or MIME types, including type/* wildcards. Client validation is advisory; validate on the server too.</summary>
    string? Accept { get; set; }
    /// <summary>Maximum bytes per file. Also enforced when opening the browser stream.</summary>
    long MaxFileSize { get; set; }
    /// <summary>Maximum number of displayed files, including existing files and failures.</summary>
    int MaxFiles { get; set; }
    /// <summary>Disables file selection and row actions without canceling existing work.</summary>
    bool Disabled { get; set; }
    /// <summary>Displays existing files without selection or actions.</summary>
    bool ReadOnly { get; set; }
    /// <summary>Displays inline image, video, and PDF previews. Defaults to true. Local object URLs are released on removal or disposal.</summary>
    bool ShowPreviews { get; set; }
    /// <summary>Enables pointer and keyboard reordering of displayed files. Defaults to true; changes are published through FilesChanged.</summary>
    bool AllowReorder { get; set; }
    /// <summary>Moves a file before another stable file key, or to the end when beforeId is null. Does not restart uploads.</summary>
    Task MoveFile(string fileId, string? beforeId);
    /// <summary>Sets the insertion point for the next browser drop. Called by the drop zone interop.</summary>
    Task SetDropTarget(string? beforeId);
    /// <summary>The drop target's prompt, followed by the underlined Browse action.</summary>
    string Label { get; set; }
    /// <summary>Optional visible helper text. When omitted, limits are provided to assistive technology without adding visual clutter.</summary>
    string? Description { get; set; }
}
