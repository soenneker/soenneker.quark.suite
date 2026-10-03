namespace Soenneker.Quark;

internal struct QuarkInt32Attribute
{
    private int _value;
    private object? _boxed;

    internal object Get(int value)
    {
        if (_boxed is null || _value != value)
        {
            _value = value;
            _boxed = QuarkAttributeValues.FromInt32(value);
        }
        return _boxed;
    }
}
