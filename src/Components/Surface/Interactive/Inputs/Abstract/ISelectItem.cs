namespace Soenneker.Quark;

/// <summary>
/// Represents a select item option component.
/// </summary>
/// <typeparam name="TValue">The type of the item value.</typeparam>
public interface ISelectItem<TValue> : IElement
{
    /// <summary>
    /// Gets or sets the value of the select item.
    /// </summary>
    TValue? Value { get; set; }

    /// <summary>
    /// Gets or sets the text content of the select item.
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// Gets or sets whether the select item is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the select item is hidden.
    /// </summary>
    bool Hidden { get; set; }
}

