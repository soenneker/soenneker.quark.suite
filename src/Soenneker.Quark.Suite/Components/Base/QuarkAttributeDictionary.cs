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

    public Dictionary<string, object> Create(int capacity = 0, IEqualityComparer<string>? comparer = null)
    {
        _useFirst = !_useFirst;
        ref var buffer = ref (_useFirst ? ref _first : ref _second);

        if (buffer is null)
        {
            buffer = new Dictionary<string, object>(capacity, comparer);
            return buffer;
        }

        buffer.Clear();
        buffer.EnsureCapacity(capacity);
        return buffer;
    }

    public Dictionary<string, object> Create(IReadOnlyDictionary<string, object> source, int extraCapacity = 0)
    {
        var attributes = Create(source.Count + extraCapacity);
        if (source is Dictionary<string, object> dictionary)
        {
            foreach (var pair in dictionary)
                attributes[pair.Key] = pair.Value;
        }
        else
        {
            foreach (var pair in source)
                attributes[pair.Key] = pair.Value;
        }
        return attributes;
    }
}
