namespace Soenneker.Quark;

/// <summary>
/// Represents a container component for responsive content layouts.
/// </summary>
public interface IContainer : IElement
{
    /// <summary>
    /// Gets or sets the container classes.
    /// </summary>
    string? ContainerType { get; set; }
}
