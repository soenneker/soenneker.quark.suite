namespace Soenneker.Quark;

/// <summary>
/// Describes a proposed new connection or endpoint change before it is accepted.
/// </summary>
public sealed class NodeEditorConnectionValidationRequest
{
    /// <summary>Gets or sets the existing edge being changed, or null when a new edge is being proposed.</summary>
    public string? EdgeId { get; set; }

    /// <summary>Gets or sets the proposed source node identifier.</summary>
    public string SourceNodeId { get; set; } = "";

    /// <summary>Gets or sets the proposed source port identifier.</summary>
    public string SourcePortId { get; set; } = "";

    /// <summary>Gets or sets the proposed target node identifier.</summary>
    public string TargetNodeId { get; set; } = "";

    /// <summary>Gets or sets the proposed target port identifier.</summary>
    public string TargetPortId { get; set; } = "";
}
