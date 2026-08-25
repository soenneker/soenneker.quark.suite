namespace Soenneker.Quark;

/// <inheritdoc cref="INodeEditorOptions"/>
public sealed class NodeEditorOptions : INodeEditorOptions
{
    public bool DraggableNodes { get; set; } = true;

    public bool PanEnabled { get; set; } = true;

    public bool MarqueeSelectionEnabled { get; set; } = true;

    public bool ZoomEnabled { get; set; } = true;

    public bool ConnectionsEnabled { get; set; } = true;

    public bool DeleteKeyEnabled { get; set; } = true;

    public bool CommandShortcutsEnabled { get; set; } = true;

    public bool InlineEdgeLabelEditingEnabled { get; set; } = true;

    public double MinZoom { get; set; } = 0.35;

    public double MaxZoom { get; set; } = 2;

    public double InitialZoom { get; set; } = 1;

    public double InitialX { get; set; }

    public double InitialY { get; set; }

    public double GridSize { get; set; } = 16;

    public bool SnapToGrid { get; set; }

    public bool FitViewOnInitialize { get; set; }
}
