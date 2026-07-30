namespace Soenneker.Quark;

/// <summary>
/// Supplies the previous and committed labels after an inline edge-label edit.
/// </summary>
public sealed class NodeEditorEdgeLabelChangedEventArgs
{
    /// <summary>Gets or sets the identifier of the edited edge.</summary>
    public string EdgeId { get; set; } = "";

    /// <summary>Gets or sets the label before the edit was committed.</summary>
    public string? PreviousLabel { get; set; }

    /// <summary>Gets or sets the committed label.</summary>
    public string? Label { get; set; }
}
