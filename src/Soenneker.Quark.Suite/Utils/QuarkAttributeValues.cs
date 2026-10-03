namespace Soenneker.Quark;

internal static class QuarkAttributeValues
{
    internal static readonly object True = true;
    internal static readonly object False = false;
    internal static readonly object Zero = 0;
    internal static readonly object One = 1;
    internal static readonly object MinusOne = -1;

    internal static object FromInt32(int value) => value switch
    {
        -1 => MinusOne,
        0 => Zero,
        1 => One,
        _ => value
    };
}
