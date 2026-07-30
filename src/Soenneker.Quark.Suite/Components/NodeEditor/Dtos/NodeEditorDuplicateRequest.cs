namespace Soenneker.Quark;

/// <summary>
/// Identifies a node the consumer should duplicate.
/// </summary>
public sealed class NodeEditorDuplicateRequest
{
    /// <summary>Gets or sets the identifier of the node to duplicate.</summary>
    public required string NodeId { get; set; }

    /// <summary>Gets or sets the suggested horizontal graph-space position for the duplicate.</summary>
    public double SuggestedX { get; set; }

    /// <summary>Gets or sets the suggested vertical graph-space position for the duplicate.</summary>
    public double SuggestedY { get; set; }
}
