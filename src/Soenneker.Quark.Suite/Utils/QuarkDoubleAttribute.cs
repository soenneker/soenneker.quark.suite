using System;

namespace Soenneker.Quark;

internal struct QuarkDoubleAttribute
{
    private long _bits;
    private object? _boxed;

    internal object Get(double value)
    {
        var bits = BitConverter.DoubleToInt64Bits(value);
        if (_boxed is null || _bits != bits)
        {
            _bits = bits;
            _boxed = value;
        }
        return _boxed;
    }
}
