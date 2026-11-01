using System;

namespace Soenneker.Quark.Suite.Tests;

public sealed class UnevaluatedValidationModel
{
    public string Name => throw new InvalidOperationException("Binding a field must not evaluate its getter.");
}
