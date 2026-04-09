using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

internal interface ISelectFocusableItem
{
    bool IsDisabledForNavigation { get; }

    string? Value { get; }

    /// <summary>
    /// Text used for Radix-style typeahead (item label).
    /// </summary>
    string? SearchText { get; }

    string ItemId { get; }

    Task FocusAsync();

    void SetActive(bool active);

    /// <summary>
    /// Commits the current item as the selection (Enter/Space on trigger while open).
    /// </summary>
    Task InvokeSelectAsync();

    /// <summary>
    /// DOM node used to scroll the active option into view inside the select viewport.
    /// </summary>
    ElementReference GetScrollAnchor();
}
