using System;

namespace Soenneker.Quark.Suite.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class GenerateQuarkThemeCssAttribute : Attribute
{
    public GenerateQuarkThemeCssAttribute(string outputFilePath)
    {
        OutputFilePath = outputFilePath;
    }

    public string OutputFilePath { get; }
}
