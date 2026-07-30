namespace Soenneker.Quark;

/// <summary>
/// Represents a positioned node container within a node editor.
/// </summary>
public interface INodeEditorNode : IElement
{
    /// <summary>
    /// Gets or sets the node model rendered by the container.
    /// </summary>
    NodeEditorNodeModel Node { get; set; }

    /// <summary>
    /// Gets or sets whether the node is currently selected.
    /// </summary>
    bool Selected { get; set; }
}
