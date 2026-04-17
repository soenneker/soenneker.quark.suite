using System;
using Soenneker.Blazor.Utils.Ids;

namespace Soenneker.Quark;

internal sealed class PopoverContext
{
    public PopoverContext()
    {
        var baseId = BlazorIdGenerator.New("quark-popover");
        TitleId = BlazorIdGenerator.Child(baseId, "title");
        DescriptionId = BlazorIdGenerator.Child(baseId, "description");
    }

    public string TitleId { get; }

    public string DescriptionId { get; }

    /// <summary>Radix <c>modal</c>: when <c>true</c>, content is exposed as a modal dialog for assistive tech.</summary>
    public bool Modal { get; set; }
}
