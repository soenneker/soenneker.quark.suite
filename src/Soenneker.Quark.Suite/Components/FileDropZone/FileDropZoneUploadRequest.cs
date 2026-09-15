using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Soenneker.Quark;

/// <summary>Access to an upload's bounded browser stream and renderer-safe stage reporting.</summary>
public sealed class FileDropZoneUploadRequest
{
    private readonly Func<FileDropZoneState, int?, string?, Task> _report;
    private readonly long _maxFileSize;
    private readonly CancellationToken _cancellationToken;

    internal FileDropZoneUploadRequest(IBrowserFile browserFile, FileDropZoneFile file, long maxFileSize,
        CancellationToken cancellationToken, Func<FileDropZoneState, int?, string?, Task> report)
    {
        BrowserFile = browserFile;
        File = file;
        _maxFileSize = maxFileSize;
        _cancellationToken = cancellationToken;
        _report = report;
    }

    /// <summary>The selected file. Treat its name, size, and MIME type as untrusted client metadata.</summary>
    public IBrowserFile BrowserFile { get; }
    /// <summary>The drop zone's metadata, including the stable UI key.</summary>
    public FileDropZoneFile File { get; }
    /// <summary>Opens the browser stream with the configured size limit and upload cancellation token. The caller disposes it.</summary>
    public Stream OpenReadStream() => BrowserFile.OpenReadStream(_maxFileSize, _cancellationToken);
    /// <summary>Opens an asynchronous-only browser stream that reports progress as bytes are read. The caller disposes it, which also disposes the underlying stream.</summary>
    /// <remarks>Read progress does not confirm server receipt or upload completion. Use the upload result to determine completion.</remarks>
    public Stream OpenReadStreamWithProgress() => new FileDropZoneProgressStream(OpenReadStream(), this);
    /// <summary>Reports bytes actually transferred by the application's transport, or null for unknown progress.</summary>
    public Task ReportUploadProgress(int? percent) => _report(FileDropZoneState.Uploading, percent, null);
    /// <summary>Reports server work separately from transfer progress. Null percent displays an indeterminate bar.</summary>
    public Task ReportProcessingProgress(string statusText, int? percent = null) => _report(FileDropZoneState.Processing, percent, statusText);
}
