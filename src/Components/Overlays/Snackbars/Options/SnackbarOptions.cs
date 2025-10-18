using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Configuration options for snackbar notifications.
/// </summary>
public sealed class SnackbarOptions
{
    /// <summary>
    /// Unique identifier for the snackbar.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// Custom content template.
    /// </summary>
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// Whether to show the close button.
    /// </summary>
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// Close button text.
    /// </summary>
    public string? CloseText { get; set; }

    /// <summary>
    /// Whether to show an action button.
    /// </summary>
    public bool ShowAction { get; set; }

    /// <summary>
    /// Action button text.
    /// </summary>
    public string? ActionText { get; set; }

    /// <summary>
    /// Auto-hide delay in milliseconds.
    /// </summary>
    public int? HideDelay { get; set; }

    /// <summary>
    /// Whether the snackbar supports multiple lines.
    /// </summary>
    public bool MultiLine { get; set; } = true;
}
