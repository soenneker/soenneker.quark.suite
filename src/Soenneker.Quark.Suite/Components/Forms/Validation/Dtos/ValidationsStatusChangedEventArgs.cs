using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Provides data for validations status changed events.
/// </summary>
/// <param name="Status">The new validation status.</param>
/// <param name="Messages">The collection of validation messages, if any.</param>
/// <param name="Validation">The validation instance that triggered the event, if applicable.</param>
public sealed record ValidationsStatusChangedEventArgs(ValidationStatus Status, IReadOnlyCollection<string>? Messages, IValidation? Validation);
