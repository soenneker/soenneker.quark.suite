namespace Soenneker.Quark;

/// <summary>
/// Represents a content panel for a specific step in a wizard.
/// </summary>
public interface IStepPanel : IElement
{
    /// <summary>
    /// Gets or sets the unique name that associates this panel with a step.
    /// </summary>
    string Name { get; set; }
}

