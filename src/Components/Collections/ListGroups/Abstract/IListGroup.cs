using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a list group component for displaying a series of content items.
/// </summary>
public interface IListGroup : IElement
{
    /// <summary>
    /// Gets or sets whether the list group should have flush styling (no borders/rounded corners).
    /// </summary>
    bool Flush { get; set; }

    /// <summary>
    /// Gets or sets whether the list group should be scrollable.
    /// </summary>
    bool Scrollable { get; set; }

    /// <summary>
    /// Gets or sets the mode of the list group (static or selectable).
    /// </summary>
    ListGroupMode Mode { get; set; }

    /// <summary>
    /// Gets or sets the selection mode (single or multiple).
    /// </summary>
    ListGroupSelectionMode SelectionMode { get; set; }

    /// <summary>
    /// Gets or sets the currently selected item (for single selection mode).
    /// </summary>
    string? SelectedItem { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the selected item changes.
    /// </summary>
    EventCallback<string> SelectedItemChanged { get; set; }

    /// <summary>
    /// Gets or sets the currently selected items (for multiple selection mode).
    /// </summary>
    List<string>? SelectedItems { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the selected items change.
    /// </summary>
    EventCallback<List<string>> SelectedItemsChanged { get; set; }

    /// <summary>
    /// Selects an item in the list group.
    /// </summary>
    /// <param name="name">The name of the item to select.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SelectItem(string name);
}

