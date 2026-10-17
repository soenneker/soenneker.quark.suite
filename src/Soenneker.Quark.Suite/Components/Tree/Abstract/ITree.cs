using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>A tree supporting an optional window over flattened visible nodes.</summary>
public interface ITree : IElement
{
    /// <summary>Reveals and focuses a visible node by its stable key.</summary>
    ValueTask FocusItem(string key);
    /// <summary>Enables measured virtualization over ItemKeys. Defaults to false.</summary>
    bool Virtualize { get; set; }
    /// <summary>Keys of visible expanded nodes in depth-first order. Omit collapsed descendants.</summary>
    IReadOnlyList<string> ItemKeys { get; set; }
    /// <summary>Renders a TreeItem whose Id equals ItemKeys[index]. Supply its Level, Expanded, PositionInSet and SetSize from the full tree.</summary>
    RenderFragment<int>? ItemTemplate { get; set; }
    /// <summary>Reports disabled nodes, including unmounted ones, for keyboard navigation.</summary>
    Func<int, bool>? ItemDisabled { get; set; }
}
