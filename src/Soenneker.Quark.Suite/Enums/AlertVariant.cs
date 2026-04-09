using System;
using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the visual style variant for an Alert component (shadcn style).
/// </summary>
/// <remarks>
/// Each variant uses CSS custom properties for theming (e.g. --alert-success, --alert-info).
/// </remarks>
[EnumValue<string>]
public sealed partial class AlertVariant
{
    /// <summary>
    /// Default alert style with neutral muted accent. General informational messages.
    /// </summary>
    public static readonly AlertVariant Default = new("default");

    /// <summary>
    /// Destructive alert style with red accent. Errors, critical warnings, or destructive actions.
    /// </summary>
    public static readonly AlertVariant Destructive = new("destructive");

    /// <summary>
    /// Legacy alias for <see cref="Destructive"/>.
    /// </summary>
    [Obsolete("Use AlertVariant.Destructive instead.")]
    public static readonly AlertVariant Danger = Destructive;
}
