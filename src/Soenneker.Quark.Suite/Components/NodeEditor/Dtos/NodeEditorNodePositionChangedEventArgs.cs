namespace Soenneker.Quark;

/// <summary>
/// Supplies the final graph-space position after a node is moved.
/// </summary>
public sealed class NodeEditorNodePositionChangedEventArgs
{
    /// <summary>Gets or sets the identifier of the moved node.</summary>
    public string NodeId { get; set; } = "";

    /// <summary>Gets or sets the final horizontal graph-space position.</summary>
    public double X { get; set; }

    /// <summary>Gets or sets the final vertical graph-space position.</summary>
    public double Y { get; set; }

    /// <summary>Gets or sets the horizontal graph-space position before the move.</summary>
    public double PreviousX { get; set; }

    /// <summary>Gets or sets the vertical graph-space position before the move.</summary>
    public double PreviousY { get; set; }
}
