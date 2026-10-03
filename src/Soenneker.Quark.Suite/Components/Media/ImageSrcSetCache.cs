using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

internal struct ImageSrcSetCache
{
    private string? _source, _value;
    private int? _intrinsicWidth;
    private IReadOnlyList<int>? _widths;

    internal string? Get(string? source, IReadOnlyList<int> widths, int? intrinsicWidth = null)
    {
        bool sameWidths = _widths?.Count == widths.Count;
        if (sameWidths && !ReferenceEquals(_widths, widths))
            for (int i = 0; sameWidths && i < widths.Count; i++)
                sameWidths = _widths![i] == widths[i];
        if (sameWidths && _source == source && _intrinsicWidth == intrinsicWidth) return _value;

        var value = ImageSources.BuildSrcSet(source, widths, intrinsicWidth);
        if (ReferenceEquals(widths, ImageSources.DefaultWidths))
            _widths = widths;
        else
        {
            var snapshot = _widths as int[];
            if (snapshot?.Length != widths.Count) snapshot = new int[widths.Count];
            for (int i = 0; i < widths.Count; i++) snapshot[i] = widths[i];
            _widths = snapshot;
        }
        _source = source;
        _intrinsicWidth = intrinsicWidth;
        return _value = value;
    }
}
