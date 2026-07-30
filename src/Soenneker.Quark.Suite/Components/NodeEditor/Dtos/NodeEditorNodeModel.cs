using System.Text.Json.Serialization;

namespace Soenneker.Quark;

/// <summary>
/// Describes a positioned node displayed by <see cref="NodeEditor"/>.
/// </summary>
public sealed class NodeEditorNodeModel
{
    /// <summary>Gets or sets the identifier that uniquely identifies this node within its editor.</summary>
    public required string Id { get; set; }

    /// <summary>Gets or sets the horizontal graph-space position in pixels.</summary>
    public double X { get; set; }

    /// <summary>Gets or sets the vertical graph-space position in pixels.</summary>
    public double Y { get; set; }

    /// <summary>Gets or sets whether the node is non-interactive and rendered with reduced emphasis.</summary>
    public bool Disabled { get; set; }

    /// <summary>Gets or sets whether the node can be selected and keyboard-focused.</summary>
    public bool Selectable { get; set; } = true;

    /// <summary>Gets or sets consumer-owned data used by the node template.</summary>
    [JsonIgnore]
    public object? Tag { get; set; }

    /// <summary>
    /// Gets or sets an optional value included in render-change detection.
    /// Change this value when mutable template data in <see cref="Tag"/> is updated in place.
    /// </summary>
    [JsonIgnore]
    public object? RenderKey { get; set; }
}
