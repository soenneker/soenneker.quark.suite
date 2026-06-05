namespace Soenneker.Quark;

/// <summary>
/// Represents the model selector option record.
/// </summary>
/// <param name="Value">The value.</param>
/// <param name="Label">The label.</param>
/// <param name="Group">The group.</param>
/// <param name="Description">The description.</param>
public sealed record ModelSelectorOption(string Value, string Label, string? Group = null, string? Description = null);
