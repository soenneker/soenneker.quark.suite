using System;

namespace Soenneker.Quark;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class GenerateQuarkThemeCssAttribute : Attribute
{
    public GenerateQuarkThemeCssAttribute(string outputFilePath)
    {
        OutputFilePath = outputFilePath;
    }

    public string OutputFilePath { get; }

    /// <summary>
    /// When true, unminified CSS is written to the output path. Can be combined with <see cref="BuildMinified"/>.
    /// </summary>
    public bool BuildUnminified { get; set; } = true;

    /// <summary>
    /// When true, minified CSS is written to a path with .min before the extension (e.g. theme.css → theme.min.css). Can be combined with <see cref="BuildUnminified"/>.
    /// </summary>
    public bool BuildMinified { get; set; } = true;
}
