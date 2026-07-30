namespace Soenneker.Quark;

/// <summary>
/// Supplies the containing node identity to composed node ports.
/// </summary>
public interface INodeEditorNodeContext
{
    /// <summary>
    /// Gets the identifier of the containing node.
    /// </summary>
    string NodeId { get; }
}
