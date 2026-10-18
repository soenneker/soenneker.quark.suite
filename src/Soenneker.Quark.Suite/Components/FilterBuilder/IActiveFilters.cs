using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>Removable summaries of active predicates, retaining the original nested group structure.</summary>
public interface IActiveFilters : IElement
{
    /// <summary>Field labels used to display predicates.</summary>
    IReadOnlyList<FilterField> Fields { get; set; }
    /// <summary>Current filter tree.</summary>
    FilterGroup Value { get; set; }
    /// <summary>Receives a replacement tree after removal or clearing.</summary>
    EventCallback<FilterGroup> ValueChanged { get; set; }
    /// <summary>Disables removal.</summary>
    bool Disabled { get; set; }
}
