namespace Soenneker.Quark;

/// <summary>
/// Minimal DOM element contract for renderable HTML-like elements.
/// </summary>
public interface IElement : IComponent
{
    /// <summary>
    /// Gets or sets the space-separated keyboard shortcuts for the element.
    /// </summary>
    string? AccessKey { get; set; }

    /// <summary>
    /// Gets or sets the BCP 47 language tag for the element and its content.
    /// </summary>
    string? Lang { get; set; }

    /// <summary>
    /// Gets or sets text direction: ltr, rtl, or auto.
    /// </summary>
    string? Dir { get; set; }

    /// <summary>
    /// Gets or sets editability: true, false, or plaintext-only. Null inherits the parent setting.
    /// </summary>
    string? ContentEditable { get; set; }

    /// <summary>
    /// Gets or sets spell checking. Null leaves browser inheritance intact; false renders explicitly.
    /// </summary>
    bool? SpellCheck { get; set; }

    /// <summary>
    /// Gets or sets automatic capitalization: none, off, on, sentences, words, or characters.
    /// </summary>
    string? AutoCapitalize { get; set; }

    /// <summary>
    /// Gets or sets automatic spelling correction. Null preserves browser defaults.
    /// </summary>
    bool? AutoCorrect { get; set; }

    /// <summary>
    /// Gets or sets the virtual keyboard enter-key hint: enter, done, go, next, previous, search, or send.
    /// </summary>
    string? EnterKeyHint { get; set; }

    /// <summary>
    /// Gets or sets the HTML keyboard hint, such as text, numeric, decimal, or none. Separate from component-specific input modes.
    /// </summary>
    string? HtmlInputMode { get; set; }

    /// <summary>
    /// Gets or sets native HTML drag behavior, independently of component-specific dragging.
    /// </summary>
    bool? HtmlDraggable { get; set; }

    /// <summary>
    /// Gets or sets whether content is eligible for translation. Separate from the CSS Translate property.
    /// </summary>
    bool? TranslateContent { get; set; }

    /// <summary>
    /// Gets or sets whether the subtree is inert to focus and user interaction.
    /// </summary>
    bool? Inert { get; set; }

    /// <summary>
    /// Gets or sets whether the browser should focus this element automatically.
    /// </summary>
    bool AutoFocus { get; set; }

    /// <summary>
    /// Gets or sets the shadow DOM slot name, independently of component-specific slots.
    /// </summary>
    string? HtmlSlot { get; set; }

    /// <summary>
    /// Gets or sets the space-separated shadow DOM part names.
    /// </summary>
    string? Part { get; set; }

    /// <summary>
    /// Gets or sets the shadow DOM part mappings exposed by the element.
    /// </summary>
    string? ExportParts { get; set; }

    /// <summary>
    /// Gets or sets native popover behavior: auto, manual, or hint.
    /// </summary>
    string? Popover { get; set; }

    /// <summary>
    /// Gets or sets the global identifier of a microdata item.
    /// </summary>
    string? HtmlItemId { get; set; }

    /// <summary>
    /// Gets or sets the space-separated microdata property names.
    /// </summary>
    string? ItemProp { get; set; }

    /// <summary>
    /// Gets or sets the space-separated IDs containing additional microdata properties.
    /// </summary>
    string? ItemRef { get; set; }

    /// <summary>
    /// Gets or sets whether the element introduces a microdata item.
    /// </summary>
    bool? ItemScope { get; set; }

    /// <summary>
    /// Gets or sets the space-separated vocabulary URLs for a microdata item.
    /// </summary>
    string? ItemType { get; set; }

    /// <summary>
    /// Gets or sets the live region announcement priority: off, polite, or assertive.
    /// </summary>
    string? AriaLive { get; set; }

    /// <summary>
    /// Gets or sets whether the entire live region is announced when part changes.
    /// </summary>
    bool? AriaAtomic { get; set; }

    /// <summary>
    /// Gets or sets whether assistive technologies should wait for updates to finish.
    /// </summary>
    bool? AriaBusy { get; set; }

    /// <summary>
    /// Gets or sets which live region changes are announced: additions, removals, text, or all.
    /// </summary>
    string? AriaRelevant { get; set; }

    /// <summary>
    /// Gets or sets the IDs of elements controlled by this element.
    /// </summary>
    string? AriaControls { get; set; }

    /// <summary>
    /// Gets or sets the IDs of elements providing extended details.
    /// </summary>
    string? AriaDetails { get; set; }

    /// <summary>
    /// Gets or sets the IDs of elements describing an error.
    /// </summary>
    string? AriaErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the IDs defining an alternative reading order.
    /// </summary>
    string? AriaFlowTo { get; set; }

    /// <summary>
    /// Gets or sets the keyboard shortcuts exposed to assistive technologies.
    /// </summary>
    string? AriaKeyShortcuts { get; set; }

    /// <summary>
    /// Gets or sets the IDs establishing an accessibility ownership relationship.
    /// </summary>
    string? AriaOwns { get; set; }

    /// <summary>
    /// Gets or sets the localized human-readable description of the role.
    /// </summary>
    string? AriaRoleDescription { get; set; }

    /// <summary>
    /// Gets or sets the accessible description.
    /// </summary>
    string? AriaDescription { get; set; }

    /// <summary>
    /// Gets or sets the label intended for braille output.
    /// </summary>
    string? AriaBrailleLabel { get; set; }

    /// <summary>
    /// Gets or sets the role description intended for braille output.
    /// </summary>
    string? AriaBrailleRoleDescription { get; set; }


    /// <summary>
    /// Gets or sets tab index.
    /// </summary>
    int? TabIndex { get; set; }
    /// <summary>
    /// Gets or sets role.
    /// </summary>
    string? Role { get; set; }
    /// <summary>
    /// Gets or sets aria label.
    /// </summary>
    string? AriaLabel { get; set; }
    /// <summary>
    /// Gets or sets aria labelled by.
    /// </summary>
    string? AriaLabelledBy { get; set; }
    /// <summary>
    /// Gets or sets aria described by.
    /// </summary>
    string? AriaDescribedBy { get; set; }
    /// <summary>
    /// Gets or sets aria current.
    /// </summary>
    string? AriaCurrent { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether aria hidden.
    /// </summary>
    bool? AriaHidden { get; set; }
}
