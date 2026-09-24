using System;

namespace Soenneker.Quark;

/// <summary>Requests generated field validation for a model, including models with no validation attributes. Annotated models are discovered automatically.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class GenerateQuarkValidationAttribute : Attribute;
