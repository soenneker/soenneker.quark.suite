using System;
using System.Collections.Generic;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ClassListProbe : RenderComponent
{
    public static void Append(Dictionary<string, object> attributes, ReadOnlySpan<string?> classes) =>
        AppendClassAttribute(attributes, classes);

    public static void AppendArray(Dictionary<string, object> attributes, string?[] classes) =>
        AppendClassAttribute(attributes, classes);
}
