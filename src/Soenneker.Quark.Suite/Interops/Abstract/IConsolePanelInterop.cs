using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Provides browser file downloads for console content.
/// </summary>
public interface IConsolePanelInterop : IAsyncDisposable
{
    /// <summary>
    /// Observes console content and scrolls the element to the latest output after mutations.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Console Panel is ready for use.</returns>
    ValueTask InitializeAutoScroll(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops observing console content for automatic scrolling.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes after the Console Panel has stopped.</returns>
    ValueTask DestroyAutoScroll(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads console text as a browser file.
    /// </summary>
    /// <param name="fileName">Name of the target file.</param>
    /// <param name="contentType">Media type describing the supplied content.</param>
    /// <param name="content">Content to render, store, or send.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the download operation is complete.</returns>
    ValueTask Download(string fileName, string contentType, string content, CancellationToken cancellationToken = default);
}
