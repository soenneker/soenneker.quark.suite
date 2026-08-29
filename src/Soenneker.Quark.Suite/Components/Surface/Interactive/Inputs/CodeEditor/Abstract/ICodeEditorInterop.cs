using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop for Monaco-based code editor: loads resources and manages editor lifecycle.
/// </summary>
public interface ICodeEditorInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures Monaco resources are loaded (JS, CSS) via CDN.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Code Editor is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a Monaco editor instance in the provided container with the given options.
    /// </summary>
    /// <param name="container">Container element to host the editor.</param>
    /// <param name="optionsJson">Monaco editor options JSON string.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that completes when the editor creation is complete.</returns>
    ValueTask CreateEditor(ElementReference container, string optionsJson, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the value (full text) of the editor instance bound to the container.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="value">CSS value used to construct the utility class.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the value has been stored.</returns>
    ValueTask SetValue(ElementReference container, string value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current value (full text) from the editor instance bound to the container.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by get Value.</returns>
    ValueTask<string?> GetValue(ElementReference container, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets language.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="language">Language for the set language operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the language has been stored.</returns>
    ValueTask SetLanguage(ElementReference container, string language, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates theme (e.g., vs-dark, vs-light).
    /// </summary>
    /// <param name="theme">Theme for the set theme operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the theme has been stored.</returns>
    ValueTask SetTheme(string theme, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes the editor instance associated with the container.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the dispose editor operation is complete.</returns>
    ValueTask DisposeEditor(ElementReference container, CancellationToken cancellationToken = default);

    /// <summary>
    /// Calls Monaco <c>layout()</c> on the editor so it picks up the container size after visibility or layout changes.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the layout operation is complete.</returns>
    ValueTask Layout(ElementReference container, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the editor height based on the current content line count.
    /// </summary>
    /// <param name="container">Container element hosting the editor.</param>
    /// <param name="minLines">Minimum number of lines to display (default: 1).</param>
    /// <param name="maxLines">Maximum number of lines to display (default: no limit).</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the content height update is complete.</returns>
    ValueTask UpdateContentHeight(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a content change listener that automatically adjusts editor height as content changes.
    /// </summary>
    /// <param name="container">Container element hosting the editor.</param>
    /// <param name="minLines">Minimum number of lines to display (default: 1).</param>
    /// <param name="maxLines">Maximum number of lines to display (default: no limit).</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the content change listener addition is complete.</returns>
    ValueTask AddContentChangeListener(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers the .NET callback used to propagate Monaco content changes.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the Code Editor.</typeparam>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="dotNetRef">JavaScript-invokable reference to the .NET component instance.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the content changed callback registration is complete.</returns>
    ValueTask RegisterContentChangedCallback<T>(ElementReference container, DotNetObjectReference<T> dotNetRef,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Enables file dropping for an editor.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="dropZone">Drop Zone for the configure file drop operation.</param>
    /// <param name="inputId">input ID to read or transform.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the configure file drop operation is complete.</returns>
    ValueTask ConfigureFileDrop(ElementReference container, ElementReference dropZone, string inputId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes file-drop handling for an editor.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the file drop removal is complete.</returns>
    ValueTask RemoveFileDrop(ElementReference container, CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts text at the most recent file-drop position, or at the current cursor position.
    /// </summary>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="text">Text to read, write, or transform.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the insert text at drop position operation is complete.</returns>
    ValueTask InsertTextAtDropPosition(ElementReference container, string text, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a .NET callback invoked when the app theme changes (light/dark).
    /// </summary>
    /// <typeparam name="T">Type of value handled by the Code Editor.</typeparam>
    /// <param name="dotNetRef">JavaScript-invokable reference to the .NET component instance.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the theme changed callback registration is complete.</returns>
    ValueTask RegisterThemeChangedCallback<T>(DotNetObjectReference<T> dotNetRef, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// Unregisters the theme-changed callback. Swallows errors if JS is no longer available.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the Code Editor.</typeparam>
    /// <param name="dotNetRef">JavaScript-invokable reference to the .NET component instance.</param>
    /// <returns>A task that completes when the theme changed callback registration has been removed.</returns>
    ValueTask UnregisterThemeChangedCallback<T>(DotNetObjectReference<T> dotNetRef)
        where T : class;
}
