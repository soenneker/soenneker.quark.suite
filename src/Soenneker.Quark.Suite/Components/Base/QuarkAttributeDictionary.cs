using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Lazily double-buffers attribute dictionaries passed to child components. Blazor can retain
/// the previous parameter value while diffing, so adjacent renders must use different instances.
/// </summary>
internal struct QuarkAttributeDictionary
{
    private Dictionary<string, object>? _first;
    private Dictionary<string, object>? _second;
    private bool _useFirst;

    public Dictionary<string, object> Create(int capacity = 0)
    {
        _useFirst = !_useFirst;
        ref var buffer = ref (_useFirst ? ref _first : ref _second);

        if (buffer is null)
        {
            buffer = new Dictionary<string, object>(capacity);
            return buffer;
        }

        buffer.Clear();
        buffer.EnsureCapacity(capacity);
        return buffer;
    }
}
