namespace Soenneker.Quark;

/// <summary>
/// Describes an empty outgoing branch that consumers can turn into a new step.
/// </summary>
public sealed class NodeEditorAddHandleModel
{
    /// <summary>Gets or sets the identifier that uniquely identifies this handle within its editor.</summary>
    public required string Id { get; set; }

    /// <summary>Gets or sets the node containing the source port for the empty branch.</summary>
    public required string SourceNodeId { get; set; }

    /// <summary>Gets or sets the source port from which the placeholder connection begins.</summary>
    public required string SourcePortId { get; set; }

    /// <summary>Gets or sets the graph-space horizontal position of the handle.</summary>
    public double X { get; set; }

    /// <summary>Gets or sets the graph-space vertical position of the handle.</summary>
    public double Y { get; set; }

    /// <summary>Gets or sets optional text displayed along the placeholder connection.</summary>
    public string? Label { get; set; }

    /// <summary>Gets or sets an accessible name for the add action.</summary>
    public string? AccessibleLabel { get; set; }

    /// <summary>Gets or sets whether the handle is unavailable.</summary>
    public bool Disabled { get; set; }
}
