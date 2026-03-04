using System;

namespace Soenneker.Quark.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class TailwindPrefixAttribute : Attribute
{
    public TailwindPrefixAttribute(string prefix) => Prefix = prefix;

    /// <summary>
    /// Prefix used to generate class names from self-referencing Chain() properties.
    /// Example: "accent-" produces accent-{auto,primary,...}.
    /// </summary>
    public string Prefix { get; }

    /// <summary>
    /// If true, generator emits breakpoint variants (sm:, md:, lg:, xl:, 2xl:).
    /// </summary>
    public bool Responsive { get; init; } = true;
}
