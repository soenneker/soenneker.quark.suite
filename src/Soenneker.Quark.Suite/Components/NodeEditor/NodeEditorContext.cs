using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="INodeEditorContext"/>
public sealed class NodeEditorContext : INodeEditorContext
{
    private readonly NodeEditor _editor;

    internal NodeEditorContext(NodeEditor editor)
    {
        _editor = editor;
    }

    public ValueTask ZoomIn() => _editor.ZoomIn();

    public ValueTask ZoomOut() => _editor.ZoomOut();

    public ValueTask FitView() => _editor.FitView();

    public ValueTask ResetView() => _editor.ResetView();

    public bool CanUndo => _editor.CanUndo;

    public bool CanRedo => _editor.CanRedo;

    public Task Undo() => _editor.Undo();

    public Task Redo() => _editor.Redo();
}
