namespace Soenneker.Quark;

/// <summary>
/// Represents an input component with common input properties.
/// </summary>
public interface IInputSurface : IInteractiveSurface
{
    /// <summary>
    /// Gets or sets whether the input is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the input is read-only.
    /// </summary>
    bool ReadOnly { get; set; }

    /// <summary>
    /// Gets or sets the name attribute for form submission.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text displayed when the input is empty.
    /// </summary>
    string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets whether the input is required.
    /// </summary>
    bool Required { get; set; }

    /// <summary>
    /// Gets or sets whether the input should automatically receive focus when the page loads.
    /// </summary>
    bool AutoFocus { get; set; }
}