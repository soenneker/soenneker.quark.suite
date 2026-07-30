namespace Soenneker.Quark;

/// <summary>
/// Describes the current graph viewport after a pan or zoom interaction.
/// </summary>
public sealed class NodeEditorViewportChangedEventArgs
{
    /// <summary>Gets or sets the horizontal viewport translation in screen pixels.</summary>
    public double X { get; set; }

    /// <summary>Gets or sets the vertical viewport translation in screen pixels.</summary>
    public double Y { get; set; }

    /// <summary>Gets or sets the viewport scale.</summary>
    public double Zoom { get; set; }
}
