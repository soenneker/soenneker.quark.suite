using System;

namespace Soenneker.Quark;

internal readonly struct ComponentCssDeclaration
{
    internal readonly string? Property;
    internal readonly ReadOnlyMemory<char> Value;

    internal ComponentCssDeclaration(string? property, ReadOnlyMemory<char> value)
    {
        Property = property;
        Value = value;
    }

    internal ComponentCssDeclaration(string property, string value) : this(property, value.AsMemory())
    {
    }
}
