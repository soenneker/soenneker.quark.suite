using System;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

internal readonly struct Accessor
{
    public readonly string CssName;
    public readonly Func<object, string?> Getter;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Accessor(string cssName, Func<object, string?> getter)
    {
        CssName = cssName;
        Getter = getter;
    }
}
