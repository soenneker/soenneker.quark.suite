using System;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>Defines a model-bound text field, its optional validation rule and optional custom editor.</summary>
public sealed record SchemaField<TModel>(string Key, string Label, Func<TModel, string?> Read,
    Action<TModel, string?> Write, Func<TModel, string?>? Validate = null, RenderFragment<TModel>? Editor = null);
