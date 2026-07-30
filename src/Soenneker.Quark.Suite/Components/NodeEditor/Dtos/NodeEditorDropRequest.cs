namespace Soenneker.Quark;

/// <summary>
/// Describes palette data dropped onto the editor at a graph-space position.
/// </summary>
public sealed class NodeEditorDropRequest
{
    /// <summary>Gets or sets the consumer-defined palette item type.</summary>
    public string Type { get; set; } = "";

    /// <summary>Gets or sets optional opaque consumer data supplied by the palette item.</summary>
    public string? Data { get; set; }

    /// <summary>Gets or sets the horizontal graph-space drop position.</summary>
    public double X { get; set; }

    /// <summary>Gets or sets the vertical graph-space drop position.</summary>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the connection under the drop point, when the item was dropped onto an existing edge.
    /// Consumers can split that edge to insert the new node.
    /// </summary>
    public string? EdgeId { get; set; }
}
