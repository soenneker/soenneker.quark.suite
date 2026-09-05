using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

// Each rendered attribute bag needs its own pair: child components can retain
// the previous dictionary until they receive the next parameter set.
internal sealed class RenderAttributeDictionary
{
    private Dictionary<string, object>? _first;
    private Dictionary<string, object>? _second;
    private bool _useFirst;

    public Dictionary<string, object> Create(IReadOnlyDictionary<string, object>? source, int extraCapacity)
    {
        _useFirst = !_useFirst;
        ref var buffer = ref (_useFirst ? ref _first : ref _second);
        int capacity = (source?.Count ?? 0) + extraCapacity;
        buffer ??= new Dictionary<string, object>(capacity, StringComparer.OrdinalIgnoreCase);
        buffer.Clear();
        buffer.EnsureCapacity(capacity);

        if (source is Dictionary<string, object> dictionary)
        {
            foreach (var pair in dictionary)
                buffer[pair.Key] = pair.Value;
        }
        else if (source is not null)
        {
            foreach (var pair in source)
                buffer[pair.Key] = pair.Value;
        }

        return buffer;
    }
}
