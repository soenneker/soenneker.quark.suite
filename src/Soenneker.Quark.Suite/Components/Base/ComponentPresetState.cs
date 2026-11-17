using System.Collections.Generic;

namespace Soenneker.Quark;

internal sealed class ComponentPresetState
{
    public QuarkPresetToken? Preset;
    public IReadOnlyList<QuarkPresetToken>? Presets;
    public QuarkPresetContext? AppliedPresetContext;
    public PresetEvaluationState? Evaluation;
    public PresetParameterBits ExplicitParameters;
}
