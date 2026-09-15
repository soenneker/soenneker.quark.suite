using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>Owns the file drop zone JavaScript module and browser preview operations. Owners isolate previews between component instances.</summary>
public interface IFileDropZoneInterop : IAsyncDisposable
{
    /// <summary>Registers pointer reordering, keyboard controls, and external-file drop feedback for one component. Repeated initialization replaces the previous registration.</summary>
    ValueTask Initialize(string owner, ElementReference element, DotNetObjectReference<FileDropZone> callbackReference,
        CancellationToken cancellationToken = default);
    /// <summary>Creates a browser-local preview URL for a selected file, or returns null when the file or media type is unavailable.</summary>
    /// <param name="owner">Unique component owner key.</param>
    /// <param name="inputId">ID of the native file input holding the selection.</param>
    /// <param name="index">Zero-based index within the native input's file collection.</param>
    /// <param name="fileId">Stable file key within the owner.</param>
    /// <param name="contentType">Resolved image, video, or PDF MIME type.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    ValueTask<string?> CreatePreview(string owner, string inputId, int index, string fileId, string contentType,
        CancellationToken cancellationToken = default);

    /// <summary>Releases this owner's object URLs except those associated with the supplied file keys.</summary>
    /// <param name="owner">Unique component owner key.</param>
    /// <param name="fileIds">File keys whose previews must remain available.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    ValueTask RetainPreviews(string owner, string[] fileIds, CancellationToken cancellationToken = default);

    /// <summary>Releases all preview URLs belonging to one component without disposing the shared module.</summary>
    /// <param name="owner">Unique component owner key.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    ValueTask Destroy(string owner, CancellationToken cancellationToken = default);
}
