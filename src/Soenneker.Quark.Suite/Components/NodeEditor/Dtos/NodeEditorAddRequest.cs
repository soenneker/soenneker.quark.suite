namespace Soenneker.Quark;

/// <summary>
/// Identifies an empty branch that the user asked to populate.
/// </summary>
public sealed class NodeEditorAddRequest
{
    /// <summary>Gets or sets the identifier of the activated add handle.</summary>
    public string HandleId { get; set; } = "";

    /// <summary>Gets or sets the identifier of the node containing the empty branch.</summary>
    public string SourceNodeId { get; set; } = "";

    /// <summary>Gets or sets the identifier of the source port for the empty branch.</summary>
    public string SourcePortId { get; set; } = "";

    /// <summary>Gets or sets the suggested horizontal graph-space position for the new node.</summary>
    public double X { get; set; }

    /// <summary>Gets or sets the suggested vertical graph-space position for the new node.</summary>
    public double Y { get; set; }
}
