using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Provides browser file downloads for console content.
/// </summary>
public interface IConsoleInterop : IAsyncDisposable
{
    /// <summary>Observes console content and scrolls the element to the latest output after mutations.</summary>
    ValueTask InitializeAutoScroll(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>Stops observing console content for automatic scrolling.</summary>
    ValueTask DestroyAutoScroll(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>Downloads console text as a browser file.</summary>
    ValueTask Download(string fileName, string contentType, string content, CancellationToken cancellationToken = default);
}
