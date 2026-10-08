using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

[InlineArray(((int)PresetProperty.Count + 63) / 64)]
internal struct PresetParameterBits
{
    private ulong _element0;
}
