namespace Soenneker.Quark;

/// <summary>
/// Native input attributes. Attributes such as accept and form overrides apply only to the corresponding HTML input types.
/// </summary>
public interface INativeInput : IInput
{
    /// <summary>Gets or sets the CaretColor utilities, including responsive and state variants.</summary>
    CssValue<CaretColorBuilder>? CaretColor { get; set; }

    /// <summary>Gets or sets the FieldSizing utilities, including responsive and state variants.</summary>
    CssValue<FieldSizingBuilder>? FieldSizing { get; set; }

    /// <summary>
    /// Gets or sets the ID of the owning form, including a form outside the control ancestry.
    /// </summary>
    string? Form { get; set; }

    /// <summary>
    /// Gets or sets browser autofill tokens, such as off, email, or section-shipping shipping street-address.
    /// </summary>
    string? AutoComplete { get; set; }

    /// <summary>
    /// Gets or sets the HTML minlength attribute.
    /// </summary>
    int? MinLength { get; set; }

    /// <summary>
    /// Gets or sets the HTML maxlength attribute.
    /// </summary>
    int? MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the HTML pattern attribute.
    /// </summary>
    string? Pattern { get; set; }

    /// <summary>
    /// Gets or sets the HTML min attribute.
    /// </summary>
    string? Min { get; set; }

    /// <summary>
    /// Gets or sets the HTML max attribute.
    /// </summary>
    string? Max { get; set; }

    /// <summary>
    /// Gets or sets the HTML step attribute.
    /// </summary>
    string? Step { get; set; }

    /// <summary>
    /// Gets or sets the HTML list attribute.
    /// </summary>
    string? List { get; set; }

    /// <summary>
    /// Gets or sets the HTML multiple attribute.
    /// </summary>
    bool? Multiple { get; set; }

    /// <summary>
    /// Gets or sets the HTML accept attribute.
    /// </summary>
    string? Accept { get; set; }

    /// <summary>
    /// Gets or sets the HTML capture attribute.
    /// </summary>
    string? Capture { get; set; }

    /// <summary>
    /// Gets or sets the HTML dirname attribute.
    /// </summary>
    string? DirName { get; set; }

    /// <summary>
    /// Gets or sets the HTML size attribute.
    /// </summary>
    int? HtmlSize { get; set; }

    /// <summary>
    /// Gets or sets the HTML inputmode attribute.
    /// </summary>
    string? InputMode { get; set; }

    /// <summary>
    /// Gets or sets the HTML formaction attribute.
    /// </summary>
    string? FormAction { get; set; }

    /// <summary>
    /// Gets or sets the HTML formenctype attribute.
    /// </summary>
    string? FormEncType { get; set; }

    /// <summary>
    /// Gets or sets the HTML formmethod attribute.
    /// </summary>
    string? FormMethod { get; set; }

    /// <summary>
    /// Gets or sets the HTML formnovalidate attribute.
    /// </summary>
    bool? FormNoValidate { get; set; }

    /// <summary>
    /// Gets or sets the HTML formtarget attribute.
    /// </summary>
    string? FormTarget { get; set; }
}
