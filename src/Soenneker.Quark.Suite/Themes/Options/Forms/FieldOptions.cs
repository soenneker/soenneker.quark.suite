
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Configuration options for field component styling.
/// </summary>
public sealed class FieldOptions : ComponentOptions
{
    public FieldOptions()
    {
        Selector = "[data-slot='field']";
    }

    /// <summary>
    /// Gets or sets field body/content styling scoped to the field.
    /// </summary>
    public FieldBodyOptions? Bodies { get; set; }

    /// <summary>
    /// Gets or sets field error styling scoped to the field.
    /// </summary>
    public ValidationErrorOptions? Errors { get; set; }

    /// <summary>
    /// Gets or sets field help/description styling scoped to the field.
    /// </summary>
    public FieldHelpOptions? Helps { get; set; }

    /// <summary>
    /// Gets or sets input styling scoped to the field.
    /// </summary>
    public InputOptions? Inputs { get; set; }

    /// <summary>
    /// Gets or sets label styling scoped to the field.
    /// </summary>
    public FieldLabelOptions? Labels { get; set; }

    /// <summary>
    /// Gets or sets textarea styling scoped to the field.
    /// </summary>
    public MemoInputOptions? MemoInputs { get; set; }

    /// <summary>
    /// Gets or sets select styling scoped to the field.
    /// </summary>
    public SelectOptions? Selects { get; set; }

    /// <summary>
    /// Gets or sets text input styling scoped to the field.
    /// </summary>
    public TextInputOptions? TextInputs { get; set; }

    /// <summary>
    /// Gets or sets validation error list styling scoped to the field.
    /// </summary>
    public ValidationErrorsOptions? ValidationErrors { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, Bodies, "[data-slot='field-content']", "[data-slot='field-content']", baseSelector);
        AddChildCssRules(buffer, Errors, "[data-slot='field-error']", "[data-slot='field-error']", baseSelector);
        AddChildCssRules(buffer, Helps, "[data-slot='field-description']", "[data-slot='field-description']", baseSelector);
        AddChildCssRules(buffer, Inputs, "[data-slot='input']", "[data-slot='input']", baseSelector);
        AddChildCssRules(buffer, Labels, "[data-slot='field-label']", "[data-slot='field-label']", baseSelector);
        AddChildCssRules(buffer, MemoInputs, "[data-slot='textarea']", "[data-slot='textarea']", baseSelector);
        AddChildCssRules(buffer, Selects, "[data-slot='select-trigger']", "[data-slot='select']", baseSelector);
        AddChildCssRules(buffer, TextInputs, "[data-slot='input']", "[data-slot='input']", baseSelector);
        AddChildCssRules(buffer, ValidationErrors, "[data-slot='validation-errors']", "[data-slot='validation-errors']", baseSelector);
    }
}
