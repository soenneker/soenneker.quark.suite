namespace Soenneker.Quark;

/// <summary>
/// Represents a connection point on a node.
/// </summary>
public interface INodeEditorPort : IElement
{
    /// <summary>
    /// Gets or sets the cascaded context for the node containing this port.
    /// </summary>
    NodeEditorNodeContext? NodeContext { get; set; }

    /// <summary>
    /// Gets or sets the identifier used by edges and connection requests to reference this port.
    /// The identifier must be unique within its containing node.
    /// </summary>
    string PortId { get; set; }

    /// <summary>
    /// Gets or sets whether this port starts or accepts connections.
    /// </summary>
    NodeEditorPortType Type { get; set; }

    /// <summary>
    /// Gets or sets the edge of the node where the port is positioned.
    /// Target ports default to the top and source ports default to the bottom.
    /// </summary>
    NodeEditorPortPlacement? Placement { get; set; }

    /// <summary>
    /// Gets or sets the relative position along the selected node edge, from 0 to 1.
    /// The default value centers the port.
    /// </summary>
    double Offset { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of edges that may use this port.
    /// A null value allows any number of connections.
    /// </summary>
    int? MaxConnections { get; set; }

    /// <summary>
    /// Gets or sets the accessible label and tooltip for the port.
    /// </summary>
    string? Label { get; set; }

    /// <summary>
    /// Gets or sets whether the port can participate in new connections.
    /// Existing edges that reference the port remain visible.
    /// </summary>
    bool Disabled { get; set; }
}
