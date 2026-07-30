namespace Soenneker.Quark;

/// <summary>
/// Identifies the currently selected graph item requested for deletion.
/// </summary>
public sealed class NodeEditorDeleteRequest
{
    /// <summary>Gets or sets the node identifier requested for deletion.</summary>
    public string? NodeId { get; set; }

    /// <summary>Gets or sets the edge identifier requested for deletion.</summary>
    public string? EdgeId { get; set; }
}
