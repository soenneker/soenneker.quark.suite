using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// JS interop for Monaco-based code editor: loads resources and manages editor lifecycle.
/// </summary>
public interface ICodeEditorInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures Monaco resources are loaded (JS, CSS) via CDN.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a Monaco editor instance in the provided container with the given options.
    /// </summary>
    /// <param name="container">Container element to host the editor.</param>
    /// <param name="optionsJson">Monaco editor options JSON string.</param>
    ValueTask CreateEditor(ElementReference container, string optionsJson, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the value (full text) of the editor instance bound to the container.
    /// </summary>
    ValueTask SetValue(ElementReference container, string value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current value (full text) from the editor instance bound to the container.
    /// </summary>
    ValueTask<string?> GetValue(ElementReference container, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the language for the model associated with the editor.
    /// </summary>
    ValueTask SetLanguage(ElementReference container, string language, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates theme (e.g., vs-dark, vs-light).
    /// </summary>
    ValueTask SetTheme(string theme, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes the editor instance associated with the container.
    /// </summary>
    ValueTask DisposeEditor(ElementReference container, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the editor height based on the current content line count.
    /// </summary>
    /// <param name="container">Container element hosting the editor.</param>
    /// <param name="minLines">Minimum number of lines to display (default: 1).</param>
    /// <param name="maxLines">Maximum number of lines to display (default: no limit).</param>
    /// <param name="cancellationToken"></param>
    ValueTask UpdateContentHeight(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a content change listener that automatically adjusts editor height as content changes.
    /// </summary>
    /// <param name="container">Container element hosting the editor.</param>
    /// <param name="minLines">Minimum number of lines to display (default: 1).</param>
    /// <param name="maxLines">Maximum number of lines to display (default: no limit).</param>
    /// <param name="cancellationToken"></param>
    ValueTask AddContentChangeListener(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default);
}