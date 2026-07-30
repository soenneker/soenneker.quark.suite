namespace Soenneker.Quark;

/// <inheritdoc cref="INodeEditorNodeContext"/>
public sealed class NodeEditorNodeContext : INodeEditorNodeContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NodeEditorNodeContext"/> class.
    /// </summary>
    /// <param name="nodeId">The identifier of the containing node.</param>
    public NodeEditorNodeContext(string nodeId)
    {
        NodeId = nodeId;
    }

    public string NodeId { get; }
}
