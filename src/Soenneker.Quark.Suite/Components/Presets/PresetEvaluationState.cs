using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

internal sealed class PresetEvaluationState
{
    private readonly QuarkPresetContext _context = new();
    private QuarkPresetToken? _preset;
    private QuarkPresetToken[] _presets = [];
    private bool _cached;
    private int _revision;

    internal QuarkPresetContext Evaluate(QuarkPresetToken? preset, IReadOnlyList<QuarkPresetToken>? presets)
    {
        int count = presets?.Count ?? 0;
        bool frozen = preset is null || preset.Value.IsFrozen;
        bool same = _cached && _revision == _context.Revision && _preset.HasValue == preset.HasValue &&
                    (!preset.HasValue || preset.Value.HasSameSnapshot(_preset!.Value)) && _presets.Length == count;
        for (int i = 0; i < count; i++)
        {
            frozen &= presets![i].IsFrozen;
            same &= i < _presets.Length && presets![i].HasSameSnapshot(_presets[i]);
        }
        if (frozen && same) return _context;

        _context.Clear();
        preset?.Apply(_context);
        for (int i = 0; i < count; i++) presets![i].Apply(_context);
        _cached = frozen;
        if (frozen)
        {
            _preset = preset;
            if (_presets.Length != count) _presets = new QuarkPresetToken[count];
            for (int i = 0; i < count; i++) _presets[i] = presets![i];
            _revision = _context.Revision;
        }
        return _context;
    }
}
