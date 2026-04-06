using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a SortableJS-backed list container.
/// </summary>
public interface ISortableList : IElement
{
    bool Disabled { get; set; }

    bool Sort { get; set; }

    int AnimationMs { get; set; }

    string? Group { get; set; }

    bool HandleOnly { get; set; }

    string ItemSelector { get; set; }

    string? HandleSelector { get; set; }

    string? FilterSelector { get; set; }

    EventCallback<SortableReorderEventArgs> OnReorder { get; set; }
}
