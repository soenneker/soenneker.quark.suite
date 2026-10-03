namespace Soenneker.Quark;

public partial class NodeEditor
{
    private struct EdgeLabelCache
    {
        private string? _source;
        private string? _target;
        private string? _label;

        internal string Get(NodeEditorEdgeModel edge)
        {
            if (_label is null || _source != edge.SourceNodeId || _target != edge.TargetNodeId)
            {
                _source = edge.SourceNodeId;
                _target = edge.TargetNodeId;
                _label = $"Connection from {_source} to {_target}";
            }
            return _label;
        }
    }
}
