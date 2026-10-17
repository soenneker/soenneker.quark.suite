using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Represents a console surface with copy and download actions.
/// </summary>
public interface IConsolePanel : IElement
{
    /// <summary>The complete plain-text console content used for copy and download actions.</summary>
    string? Text { get; set; }

    /// <summary>Resolves the latest plain-text console content when an action is invoked.</summary>
    Func<string?>? TextProvider { get; set; }

    /// <summary>The filename used when downloading the console content.</summary>
    string DownloadFileName { get; set; }

    /// <summary>The MIME type used when downloading the console content.</summary>
    string DownloadContentType { get; set; }

    /// <summary>Shows the download action.</summary>
    bool ShowDownload { get; set; }

    /// <summary>Shows the copy action.</summary>
    bool ShowCopy { get; set; }

    /// <summary>Disables the built-in actions.</summary>
    bool ActionsDisabled { get; set; }

    /// <summary>Text displayed by the download action.</summary>
    string DownloadLabel { get; set; }

    /// <summary>Text displayed by the copy action.</summary>
    string CopyLabel { get; set; }

    /// <summary>Text exposed after a successful copy.</summary>
    string CopiedLabel { get; set; }

    /// <summary>Icon displayed by the download action.</summary>
    LucideIcon DownloadIcon { get; set; }

    /// <summary>Icon displayed by the copy action.</summary>
    LucideIcon CopyIcon { get; set; }

    /// <summary>Visual variant used by the built-in actions.</summary>
    ButtonVariant ActionVariant { get; set; }

    /// <summary>Size used by the built-in actions.</summary>
    CssValue<ButtonSizeBuilder>? ActionSize { get; set; }

    /// <summary>Optional actions rendered after the built-in actions.</summary>
    RenderFragment? Actions { get; set; }

    /// <summary>Follows new output while the user is at the bottom. Scrolling up pauses following; Jump to latest resumes it.</summary>
    bool AutoScroll { get; set; }

    /// <summary>Fixed console height below the medium breakpoint.</summary>
    string SmallScreenHeight { get; set; }

    /// <summary>Fixed console height at the medium breakpoint and above.</summary>
    string MediumScreenHeight { get; set; }

    /// <summary>Raised after the log is downloaded.</summary>
    EventCallback<string> OnDownload { get; set; }

    /// <summary>Raised when downloading the log fails.</summary>
    EventCallback<Exception?> OnDownloadError { get; set; }

    /// <summary>Raised after the log is copied.</summary>
    EventCallback<string> OnCopySuccess { get; set; }

    /// <summary>Raised when copying the log fails.</summary>
    EventCallback<Exception?> OnCopyError { get; set; }

    /// <summary>Receives the measured viewport from the console interop. Intended for browser callbacks.</summary>
    Task SetWindow(int start, int count, double top, double bottom, bool paused);

    /// <summary>Invokes the older-page callback, serializing concurrent browser requests.</summary>
    Task RequestOlder();

    /// <summary>Stable, unique keys in chronological order. When supplied with ItemTemplate, only a viewport window is rendered. Keep keys stable when prepending or appending logs.</summary>
    IReadOnlyList<string>? ItemKeys { get; set; }

    /// <summary>Renders the entry at the supplied index in ItemKeys. ChildContent remains available for loading and empty states.</summary>
    RenderFragment<int>? ItemTemplate { get; set; }

    /// <summary>Requests an older page when scrolling near the beginning. Prepend entries with stable keys; the viewport preserves the visible entry.</summary>
    EventCallback LoadOlder { get; set; }

    /// <summary>Whether an older page is available. Defaults to false.</summary>
    bool HasOlderItems { get; set; }

    /// <summary>Enables expandable exception formatting for child ConsoleLogEntry components. Defaults to false. Copy and download text is unaffected.</summary>
    bool FormatExceptions { get; set; }

    /// <summary>Presents detected JSON objects and arrays as collapsed, expandable details, including JSON encoded as a quoted string, in child ConsoleLogEntry components. Defaults to false. Copy and download text is unaffected.</summary>
    bool FormatJson { get; set; }

    /// <summary>Wraps expanded exception and JSON lines by default. Defaults to true. When ShowWrapToggle is false, this setting fixes the wrapping behavior.</summary>
    bool WrapLines { get; set; }

    /// <summary>Shows a compact wrapping control at the top right of expanded exception and JSON details. Defaults to true.</summary>
    bool ShowWrapToggle { get; set; }
}
