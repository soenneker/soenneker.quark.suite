namespace Soenneker.Quark;

/// <summary>
/// Identifies the ports involved in a requested connection.
/// </summary>
public sealed class NodeEditorConnectionRequest
{
    /// <summary>Gets or sets the source node identifier.</summary>
    public string SourceNodeId { get; set; } = "";

    /// <summary>Gets or sets the source port identifier.</summary>
    public string SourcePortId { get; set; } = "";

    /// <summary>Gets or sets the target node identifier.</summary>
    public string TargetNodeId { get; set; } = "";

    /// <summary>Gets or sets the target port identifier.</summary>
    public string TargetPortId { get; set; } = "";
}
