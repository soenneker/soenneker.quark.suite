using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>A combobox option surface with full-data keyboard navigation in virtual mode.</summary>
public interface IComboboxList : IElement
{
    /// <summary>Filters Options by the combobox search. Disable when the caller already supplies filtered options.</summary>
    bool FilterOptions { get; set; }
    /// <summary>Windows Options while preserving disabled options, selection, and keyboard navigation. Defaults to false.</summary>
    bool Virtualize { get; set; }
    /// <summary>All choices with unique values. Search filters labels and values before virtualization.</summary>
    IReadOnlyList<SelectOption>? Options { get; set; }
    /// <summary>Optional option contents. The list supplies the accessible ComboboxItem wrapper.</summary>
    RenderFragment<SelectOption>? OptionTemplate { get; set; }
}
