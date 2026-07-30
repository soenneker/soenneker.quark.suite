namespace Soenneker.Quark;

/// <summary>
/// Describes new endpoints requested for an existing edge.
/// </summary>
public sealed class NodeEditorConnectionChangeRequest
{
    /// <summary>Gets or sets the identifier of the edge being changed.</summary>
    public string EdgeId { get; set; } = "";

    /// <summary>Gets or sets the requested source node identifier.</summary>
    public string SourceNodeId { get; set; } = "";

    /// <summary>Gets or sets the requested source port identifier.</summary>
    public string SourcePortId { get; set; } = "";

    /// <summary>Gets or sets the requested target node identifier.</summary>
    public string TargetNodeId { get; set; } = "";

    /// <summary>Gets or sets the requested target port identifier.</summary>
    public string TargetPortId { get; set; } = "";
}
