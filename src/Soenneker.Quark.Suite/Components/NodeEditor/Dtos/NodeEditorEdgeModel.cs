namespace Soenneker.Quark;

/// <summary>
/// Describes a directed connection between two node ports.
/// </summary>
public sealed class NodeEditorEdgeModel
{
    /// <summary>Gets or sets the identifier that uniquely identifies this edge within its editor.</summary>
    public required string Id { get; set; }

    /// <summary>Gets or sets the identifier of the node where the connection begins.</summary>
    public required string SourceNodeId { get; set; }

    /// <summary>Gets or sets the identifier of the source port rendered by <see cref="NodeEditorPort"/>.</summary>
    public required string SourcePortId { get; set; }

    /// <summary>Gets or sets the identifier of the node where the connection ends.</summary>
    public required string TargetNodeId { get; set; }

    /// <summary>Gets or sets the identifier of the target port rendered by <see cref="NodeEditorPort"/>.</summary>
    public required string TargetPortId { get; set; }

    /// <summary>Gets or sets optional text displayed along the connection.</summary>
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets whether the label can be edited directly from the connection.
    /// Set this to <c>false</c> when the label is owned by another domain object, such as a decision outcome.
    /// </summary>
    public bool LabelEditable { get; set; } = true;

    /// <summary>
    /// Gets or sets an optional accessible name for the connection.
    /// When omitted, the label or source and target node identifiers are used.
    /// </summary>
    public string? AccessibleLabel { get; set; }

    /// <summary>Gets or sets whether the edge is non-interactive and rendered with reduced emphasis.</summary>
    public bool Disabled { get; set; }

    /// <summary>Gets or sets whether the edge can be selected.</summary>
    public bool Selectable { get; set; } = true;
}
