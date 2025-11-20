using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Provides data for validation status changed events.
/// </summary>
/// <param name="Status">The new validation status.</param>
/// <param name="Messages">The collection of validation messages, if any.</param>
public sealed record ValidationStatusChangedEventArgs(ValidationStatus Status, IEnumerable<string>? Messages);
