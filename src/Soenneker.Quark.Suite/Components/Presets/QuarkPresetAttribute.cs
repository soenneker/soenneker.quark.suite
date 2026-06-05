using System;

namespace Soenneker.Quark;

/// <summary>
/// Represents the quark preset attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class QuarkPresetAttribute : Attribute
{
    public QuarkPresetAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets name.
    /// </summary>
    public string Name { get; }
}
