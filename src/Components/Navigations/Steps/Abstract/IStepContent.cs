using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a container for step content panels.
/// </summary>
public interface IStepContent : IElement
{
    /// <summary>
    /// Gets or sets the name of the currently selected panel.
    /// </summary>
    string SelectedPanel { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the selected panel changes.
    /// </summary>
    EventCallback<string> SelectedPanelChanged { get; set; }

    /// <summary>
    /// Selects a specific panel by name.
    /// </summary>
    /// <param name="name">The name of the panel to select.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SelectPanel(string name);
}

