namespace Soenneker.Quark;

/// <summary>
/// Represents a Bootstrap container component for responsive fixed-width layouts.
/// </summary>
public interface IContainer : IElement
{
    /// <summary>
    /// Gets or sets the container type configuration.
    /// </summary>
    CssValue<ContainerBuilder>? ContainerType { get; set; }
}