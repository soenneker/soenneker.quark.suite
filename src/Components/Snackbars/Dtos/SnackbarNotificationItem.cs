using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a notification item in the snackbar stack.
/// </summary>
/// <param name="Key">Unique identifier for the notification.</param>
/// <param name="Message">The notification message.</param>
/// <param name="Title">Optional title for the notification.</param>
/// <param name="Color">The color theme for the notification.</param>
/// <param name="Content">Optional custom content template.</param>
/// <param name="ShowClose">Whether to show the close button.</param>
/// <param name="CloseText">Text for the close button.</param>
/// <param name="ShowAction">Whether to show an action button.</param>
/// <param name="ActionText">Text for the action button.</param>
/// <param name="HideDelay">Auto-hide delay in milliseconds.</param>
/// <param name="Visible">Whether the notification is visible.</param>
public sealed record SnackbarNotificationItem(
    string Key,
    string Message,
    string? Title,
    CssValue<BackgroundColorBuilder> Color,
    RenderFragment? Content,
    bool ShowClose,
    string? CloseText,
    bool ShowAction,
    string? ActionText,
    int? HideDelay,
    bool Visible = true);
