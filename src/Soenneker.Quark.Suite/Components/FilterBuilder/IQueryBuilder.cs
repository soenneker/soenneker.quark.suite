using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>Edits nested typed predicates without executing application queries.</summary>
public interface IQueryBuilder : IElement, IAutoSaveInput<FilterGroup>
{
    /// <summary>Allowed fields and choices. Keys must be unique.</summary>
    IReadOnlyList<FilterField> Fields { get; set; }
    /// <summary>Current filter tree.</summary>
    FilterGroup Value { get; set; }
    /// <summary>Receives a replacement tree after every edit.</summary>
    EventCallback<FilterGroup> ValueChanged { get; set; }
    /// <summary>Prevents editing.</summary>
    bool Disabled { get; set; }
    /// <summary>Maximum nesting depth exposed by the editor.</summary>
    int MaxDepth { get; set; }
}
