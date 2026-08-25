using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Identifies the currently selected graph item requested for deletion.
/// </summary>
public sealed class NodeEditorDeleteRequest
{
    /// <summary>Gets or sets all node identifiers requested for deletion.</summary>
    public IReadOnlyList<string> NodeIds { get; set; } = [];

    /// <summary>Gets or sets the node identifier requested for deletion.</summary>
    /// <remarks>For a multi-node selection, this is the primary selected node and <see cref="NodeIds"/> contains the complete selection.</remarks>
    public string? NodeId { get; set; }

    /// <summary>Gets or sets the edge identifier requested for deletion.</summary>
    public string? EdgeId { get; set; }
}
