using System;

namespace Soenneker.Quark;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class QuarkPresetAttribute : Attribute
{
    public QuarkPresetAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
